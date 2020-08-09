using Code.Infrastructure.Game;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using Code.Presentation.Game.Projectile;
using Code.Presentation.Game.Saucer;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class SaucerInstaller : MonoInstaller
    {
        public GameObject SaucerProjectile;
        public GameObject BigSaucer;
        public GameObject SmallSaucer;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindViewModel();
            BindPools();
            BindServices();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<NewSaucerNotification>().OptionalSubscriber();
        }

        private void BindViewModel()
        {
            Container.BindInterfacesTo<SaucersViewModel>().AsSingle();
        }

        private void BindPools()
        {
            Container.BindMemoryPool<EnemyProjectile, EnemyProjectile.Pool<EnemyProjectile>>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(SaucerProjectile)
                .UnderTransformGroup("ProjectilePool");
            
            Container.BindMemoryPool<BigSaucer, BigSaucer.Pool>()
                .WithId(SaucerType.Big)
                .WithInitialSize(5)
                .FromComponentInNewPrefab(BigSaucer)
                .UnderTransformGroup("SaucerPool"); 
            
            Container.BindMemoryPool<SmallSaucer, SmallSaucer.Pool>()
                .WithId(SaucerType.Small)
                .WithInitialSize(5)
                .FromComponentInNewPrefab(SmallSaucer)
                .UnderTransformGroup("SaucerPool");
        }
               
        private void BindServices()
        {
            Container.BindInterfacesTo<SaucersAppearanceService>().AsSingle();
        }
    }

}