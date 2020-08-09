using Zenject;

namespace Code.Presentation.Game.Projectile
{
    public class PlayerProjectile : Projectile
    {
        [Inject] private PlayerProjectile.Pool<PlayerProjectile> _projectilePool; 
        
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