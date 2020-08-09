using Code.Configurations;
using Code.Domain.Game;
using Code.Infrastructure.Game;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using Code.Presentation.Game.Asteroid;
using Code.Presentation.Game.SmallElements;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class AsteroidInstaller : MonoInstaller
    {
        public GameObject BigAsteroidPrefab;
        public GameObject MediumAsteroidPrefab;
        public GameObject SmallAsteroidPrefab;
        public GameObject ExplosionPrefab;
        
        [SerializeField] private GameConfiguration _gameConfiguration;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindPools();
            BindEntities();
            BindViewModels();
            BindServices();
            BindConfigurations();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<NewAsteroidsNotification>().OptionalSubscriber();
            Container.DeclareSignal<NewDestroyedAsteroidsNotification>().OptionalSubscriber();
        }
        
        private void BindConfigurations()
        {
            Container.BindInstance(_gameConfiguration);
        }

        private void BindEntities()
        {
            Container.BindInterfacesTo<Galaxy>().AsSingle();
            Container.BindInterfacesTo<RandomSaucerHelper>().AsTransient();
        }

        private void BindViewModels()
        {
            Container.BindInterfacesTo<AsteroidsViewModel>().AsSingle();
        }

        private void BindServices()
        {
            Container.BindInterfacesTo<AsteroidsPerLevelService>().AsTransient();
        }

        private void BindPools()
        {
            Container.BindMemoryPool<Asteroid, Asteroid.Pool>()
                .WithId(AsteroidType.Big)
                .WithInitialSize(20)
                .FromComponentInNewPrefab(BigAsteroidPrefab)
                .UnderTransformGroup("BigAsteroidsPool");
            
            Container.BindMemoryPool<Asteroid, Asteroid.Pool>()
                .WithId(AsteroidType.Medium)
                .WithInitialSize(20)
                .FromComponentInNewPrefab(MediumAsteroidPrefab)
                .UnderTransformGroup("MediumAsteroidsPool");
            
            Container.BindMemoryPool<Asteroid, Asteroid.Pool>()
                .WithId(AsteroidType.Small)
                .WithInitialSize(20)
                .FromComponentInNewPrefab(SmallAsteroidPrefab)
                .UnderTransformGroup("SmallAsteroidsPool");
            
            Container.BindMemoryPool<Explosion, Explosion.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(ExplosionPrefab)
                .UnderTransformGroup("EffectsPool");
        }
    }
}