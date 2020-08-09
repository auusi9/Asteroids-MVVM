using Code.Configurations;
using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Presentation.Game.SmallElements;
using Code.Presentation.Game.Spaceship;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public abstract class Saucer : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SaucerShoot _saucerShoot;

        [Inject] protected GameConfiguration _gameConfiguration;
        [Inject] private ISpaceship _spaceship;
        [Inject] private Explosion.Pool _explosionPool;
        [Inject] private SaucersView _saucersView;
        [Inject] protected ISaucersViewModel _saucersViewModel;
        [Inject] protected ISpaceshipViewModel _spaceshipViewModel;
        [Inject] private SignalBus _signalBus;

        private Vector2 _direction;
        private Vector3 _objective;
        private bool _destroyed;
        private float _lastShoot;

        protected abstract Saucer.Pool _pool { get; }
        protected abstract float _velocity { get; }
        protected abstract float _shootInterval { get; }

        [Inject]
        private void Construct()
        {
        }

        public void Initialize(SaucerConfig saucerConfig)
        {
            _signalBus.Subscribe<SpaceshipDiedNotification>(SaucerDestroyed);
            transform.position = saucerConfig.Origin;
            _direction = (saucerConfig.Destination - saucerConfig.Origin).normalized;
            _objective = saucerConfig.Destination;
            _lastShoot = Time.time;
            _destroyed = false;
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
            
            SaucerDestroyedCommand();
            _explosionPool.Spawn(transform.position);
            SaucerDestroyed();
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _velocity;

            if (Vector3.Distance(_objective, transform.position) < 3f)
            {
                _signalBus.TryUnsubscribe<SpaceshipDiedNotification>(SaucerDestroyed);
                _saucersView.NewDestination(this);
            }

            if (Time.time - _lastShoot > _shootInterval)
            {
                _saucerShoot.Shoot();
                _lastShoot = Time.time;
            }
        }

        private void SaucerDestroyed()
        {
            if (_destroyed)
            {
                return;
            }

            _destroyed = true;
            Despawn();
        }

        private void Despawn()
        {
            _signalBus.TryUnsubscribe<SpaceshipDiedNotification>(SaucerDestroyed);
            _pool.Despawn(this);
        }

        protected abstract void SaucerDestroyedCommand();


        public class Pool : MonoMemoryPool<SaucerConfig, Saucer>
        {
            protected override void Reinitialize(SaucerConfig config, Saucer saucer)
            {
                saucer.Initialize(config);
            }
        }
    }

    public enum SaucerType
    {
        Big,
        Small
    }
}