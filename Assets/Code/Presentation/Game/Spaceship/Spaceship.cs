using Code.Configurations;
using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Presentation.Game.Projectile;
using Code.Presentation.Game.SmallElements;
using Code.Presentation.UI.GameOver;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Spaceship
{
    public class Spaceship : MonoBehaviour, ISpaceship
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem _particleSystemRight;
        [SerializeField] private ParticleSystem _particleSystemLeft;
    
        [Inject] private SpaceShipConfiguration _spaceShipConfiguration;
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;
        [Inject] private PlayerProjectile.Pool<PlayerProjectile> _projectilePool;
        [Inject] private Explosion.Pool _explosionPool;
        [Inject] private GameOverMenuView.Factory _gameOverMenu;
        [Inject] private SignalBus _signalBus;

        private bool _inmortal = false;
        private int _dieTrigger = Animator.StringToHash("Die");
        private int _respawn = Animator.StringToHash("Respawn");

        public Vector3 Position
        {
            get { return transform.position; }
        }
        
        public bool Inmortal
        {
            get { return _inmortal; }
        }

        private void Start()
        {
            _signalBus.Subscribe<ImpulseSpaceshipNotification>(Impulse);
            _signalBus.Subscribe<RotateSpaceshipLeftNotification>(RotateLeft);
            _signalBus.Subscribe<RotateSpaceshipRightNotification>(RotateRight);
            _signalBus.Subscribe<SpaceshipShootNotification>(Shoot);
            _signalBus.Subscribe<RespawnSpaceshipNotification>(RespawnSpaceship);
            _signalBus.Subscribe<SpaceshipDiedNotification>(SpaceshipDead);
        }

        public void Impulse()
        {
            _rigidbody.AddForce(_spaceShipConfiguration.ImpulseForce * transform.up);
            _particleSystemRight.Emit(1);
            _particleSystemLeft.Emit(1);
        }

        public void RotateLeft()
        {
            _rigidbody.MoveRotation(_rigidbody.rotation + _spaceShipConfiguration.RotationDegrees);
        }

        public void RotateRight()
        {
            _rigidbody.MoveRotation(_rigidbody.rotation - _spaceShipConfiguration.RotationDegrees);
        }

        public void Shoot()
        {
            _projectilePool.Spawn(_bulletSpawnPosition.position, transform.up * _spaceShipConfiguration.ProjectileVelocity);
        }

        //Called from animation
        public void RespawnAnimationOver()
        {
            _inmortal = false;
        }

        private void RespawnSpaceship()
        {
            transform.position = Vector3.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger(_respawn);
            ClearVisuals();
        }
        
        private void SpaceshipDead()
        {
            _signalBus.TryUnsubscribe<ImpulseSpaceshipNotification>(Impulse);
            _signalBus.TryUnsubscribe<RotateSpaceshipLeftNotification>(RotateLeft);
            _signalBus.TryUnsubscribe<RotateSpaceshipRightNotification>(RotateRight);
            _signalBus.TryUnsubscribe<SpaceshipShootNotification>(Shoot);
            _signalBus.TryUnsubscribe<RespawnSpaceshipNotification>(RespawnSpaceship);
            _signalBus.TryUnsubscribe<SpaceshipDiedNotification>(SpaceshipDead);

            _animator.SetTrigger(_dieTrigger);
            _rigidbody.velocity = Vector2.zero;
            ClearVisuals();

            _gameOverMenu.Create();
        }

        private void ClearVisuals()
        {
            _particleSystemLeft.Clear();
            _particleSystemRight.Clear();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_inmortal)
            {
                return;
            }
            
            _explosionPool.Spawn(transform.position);
            _spaceshipViewModel.SpaceShipDamagedCommand();
            _inmortal = true;
        }

        private void Update()
        {
            if (_rigidbody.velocity.magnitude > _spaceShipConfiguration.MaxVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _spaceShipConfiguration.MaxVelocity;
            }
        }
    }
}
