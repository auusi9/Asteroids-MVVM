using System.Collections;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.SmallElements
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        [Inject] private Explosion.Pool _explosionPool;

        [Inject]
        private void Construct()
        {
        }

        public void Initialize(Vector3 position)
        {
            transform.position = position;
            StartCoroutine(DespawnProjectile());
        }

        private IEnumerator DespawnProjectile()
        {
            yield return new WaitForSeconds(_particleSystem.main.duration);
            _explosionPool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Vector3, Explosion>
        {
            protected override void Reinitialize(Vector3 position, Explosion projectile)
            {
                projectile.Initialize(position);
            }
        }
    }
}