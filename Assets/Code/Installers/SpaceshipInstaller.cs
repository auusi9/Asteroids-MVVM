using Code.Configurations;
using Code.Domain.Game;
using Code.Infrastructure.Game;
using Code.Infrastructure.Notifications;
using Code.Presentation.Game.Projectile;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class SpaceshipInstaller : MonoInstaller
    {
        public GameObject Projectileprefab;
        [SerializeField] private SpaceShipConfiguration _spaceShipConfiguration;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindConfiguration();
            BindPools();
            BindViewModel();
            BindEntities();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<ImpulseSpaceshipNotification>().OptionalSubscriber();
            Container.DeclareSignal<RotateSpaceshipLeftNotification>().OptionalSubscriber();
            Container.DeclareSignal<RotateSpaceshipRightNotification>().OptionalSubscriber();
            Container.DeclareSignal<SpaceshipShootNotification>().OptionalSubscriber();
            Container.DeclareSignal<SpaceshipLivesAmountNotification>().OptionalSubscriber();
            Container.DeclareSignal<SpaceshipDiedNotification>().OptionalSubscriber();
            Container.DeclareSignal<RespawnSpaceshipNotification>().OptionalSubscriber();
            Container.DeclareSignal<ScoreUpdatedNotification>().OptionalSubscriber();
        }

        private void BindEntities()
        {
            Container.BindInterfacesTo<Player>().AsSingle().WithArguments(_spaceShipConfiguration.Lives);
        }

        private void BindPools()
        {
            Container.BindMemoryPool<PlayerProjectile, PlayerProjectile.Pool<PlayerProjectile>>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(Projectileprefab)
                .UnderTransformGroup("ProjectilePool");
        }

        private void BindConfiguration()
        {
            Container.BindInstance(_spaceShipConfiguration);
        }
        
        private void BindViewModel()
        {
            Container.BindInterfacesTo<SpaceshipViewModel>().AsSingle();
        }
    }
}