using Code.Configurations;
using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Presentation.Game.SmallElements;
using Code.Presentation.Game.Spaceship;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Asteroid
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AsteroidType _type;
        
        [Inject] private IAsteroidsViewModel _asteroidsViewModel;
        [Inject] private ISpaceship _spaceship;
        [Inject] private Explosion.Pool _explosionPool;

        private EnemyConfiguration _configuration;
        private Pool _pool;
        private bool _destroyed;
        private Vector2 _velocity;
        
        [Inject]
        private void Construct()
        {
            transform.position = Vector3.zero;
            _rigidbody.velocity = Vector2.zero;
        }

        public void Initialize(Vector3 position, Vector2 direction, EnemyConfiguration configuration, Pool pool)
        {
            transform.position = position;
            _velocity = direction * configuration.Velocity;
            _configuration = configuration;
            _pool = pool;
            _destroyed = false;
        }

        private void Update()
        {
            _rigidbody.velocity = _velocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_spaceship.Inmortal)
                {
                    return;
                }
            }
            
            AsteroidDestroyed();
        }
        
        private void AsteroidDestroyed()
        {
            if (_destroyed)
            {
                return;
            }

            _explosionPool.Spawn(transform.position);
            _asteroidsViewModel.AsteroidDestroyedCommand(_type, transform.position, _configuration.DestroyScore);
            _destroyed = true;
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Vector3, Vector2, EnemyConfiguration, Asteroid> 
        {
            protected override void Reinitialize(Vector3 position, Vector2 direction, EnemyConfiguration configuration, Asteroid asteroid)
            {
                asteroid.Initialize(position, direction, configuration, this);
            }
        }
    }

    public enum AsteroidType
    {
        Big,
        Medium,
        Small
    }
}