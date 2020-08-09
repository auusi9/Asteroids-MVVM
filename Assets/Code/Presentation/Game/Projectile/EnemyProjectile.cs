using Zenject;

namespace Code.Presentation.Game.Projectile
{
    public class EnemyProjectile : Projectile
    {
        [Inject] private EnemyProjectile.Pool<EnemyProjectile> _projectilePool; 
        
        protected override void ReturnToPool()
        {
            if (_returnedToPool)
            {
                return;
            }

            _returnedToPool = true;
            _projectilePool.Despawn(this);
        }
    }
}