using Code.Configurations;
using Code.Presentation.Game.Projectile;
using Code.Presentation.Game.Spaceship;
using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public class SaucerAccurateShoot : SaucerShoot
    {
        [Inject] private ISpaceship _spaceship;
        [Inject] private GameConfiguration _gameConfiguration;
        [Inject] private EnemyProjectile.Pool<EnemyProjectile> _pool;
        
        public override void Shoot()
        {
            _pool.Spawn(transform.position, (_spaceship.Position - transform.position).normalized * _gameConfiguration.SmallSaucerConfiguration.ProjectileVelocity);
        }
    }
}