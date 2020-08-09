using System.Collections;
using Code.Configurations;
using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Projectile
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private KeepInsideCameraView _insideCamera;
        [SerializeField] private string _compareTag;
        
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;
        [Inject] private SpaceShipConfiguration _spaceShipConfiguration;

        protected bool _returnedToPool = false;
        
        [Inject]
        private void Construct()
        {
            _insideCamera.Teleported += InsideCameraTeleportedUs;
        }

        private void OnDestroy()
        {
            _insideCamera.Teleported -= InsideCameraTeleportedUs;
        }

        public void Initialize(Vector3 position, Vector2 velocity)
        {
            transform.position = position;
            _rigidbody.velocity = velocity;
            _returnedToPool = false;
            _trail.Clear();
            StartCoroutine(DespawnProjectile());
        }

        private IEnumerator DespawnProjectile()
        {
            yield return new WaitForSeconds(_spaceShipConfiguration.ProjectileLife);
            ReturnToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(_compareTag))
            {
                ReturnToPool();
            }
        }

        protected abstract void ReturnToPool();
        
        private void InsideCameraTeleportedUs()
        {
            _trail.Clear();
        }

        public class Pool<T> : MonoMemoryPool<Vector3, Vector2, Projectile>
        {
            protected override void Reinitialize(Vector3 position, Vector2 velocity, Projectile projectile)
            {
                projectile.Initialize(position, velocity);
            }
        }
    }
}