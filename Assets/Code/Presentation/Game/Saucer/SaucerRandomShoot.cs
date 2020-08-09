using Code.Configurations;
using Code.Presentation.Game.Projectile;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public class SaucerRandomShoot : SaucerShoot
    {
        [Inject] private GameConfiguration _gameConfiguration;
        [Inject] private EnemyProjectile.Pool<EnemyProjectile> _pool;

        public override void Shoot()
        {
            _pool.Spawn(transform.position,
                Random.insideUnitCircle.normalized * _gameConfiguration.BigSaucerConfiguration.ProjectileVelocity);
        }
    }
}