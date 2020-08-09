using Code.Configurations;
using Code.Domain.Game;
using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Presentation.Game.Spaceship;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Asteroid
{
    public class AsteroidsView : MonoBehaviour
    {
        [Inject] private Camera _camera;        
        [Inject] private ISpaceship _spaceship;        
        [Inject] private IAsteroidsViewModel _asteroidsViewModel;
        [Inject] private GameConfiguration _gameConfiguration;
        [Inject(Id = AsteroidType.Big)] private Asteroid.Pool _bigAsteroidsPool;
        [Inject(Id = AsteroidType.Medium)] private Asteroid.Pool _mediumAsteroidsPool;
        [Inject(Id = AsteroidType.Small)] private Asteroid.Pool _smallAsteroidsPool;
        [Inject] private SignalBus _signalBus;
        
        private void Start()
        {
            _signalBus.Subscribe<NewAsteroidsNotification>(NewAsteroids);
            _signalBus.Subscribe<NewDestroyedAsteroidsNotification>(NewDestroyedAsteroids);
            _asteroidsViewModel.InitializeGalaxyCommand();
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<NewAsteroidsNotification>(NewAsteroids);
            _signalBus.TryUnsubscribe<NewDestroyedAsteroidsNotification>(NewDestroyedAsteroids);
        }

        private void NewAsteroids(NewAsteroidsNotification not)
        {
            for (int i = 0; i < not.NewAsteroidsDto.Count; i++)
            {
                GetPoolByType(not.NewAsteroidsDto.Type).Spawn
                (
                    GetRandomPositionInsidePlayField(), 
                    GetRandomDirection(),
                    _gameConfiguration.GetAsteroidConfigurationByType(not.NewAsteroidsDto.Type)
                );
            }
        }

        private Vector3 GetRandomPositionInsidePlayField()
        {
            Vector2 worldBoundsSize = _camera.WorldBoundsSize();

            Vector3 center = _camera.transform.position;

            return ValidSpawnPoint(center, worldBoundsSize);
        }

        private Vector3 ValidSpawnPoint(Vector3 center, Vector2 size)
        {
            bool valid = false;

            Vector3 spawnPoint = Vector3.zero;
            while (!valid)
            {
                spawnPoint = RandomPointInBox(center, size);
                valid = Vector3.Distance(_spaceship.Position, spawnPoint) > _gameConfiguration.AsteroidSpawnDistance;
            }

            return spawnPoint;
        }

        private static Vector3 RandomPointInBox(Vector3 center, Vector2 size)
        {
            Vector3 randomPoint = center + new Vector3(
                                      (Random.value - 0.5f) * size.x,
                                      (Random.value - 0.5f) * size.y
                                  );
            randomPoint.z = 0f;
            return randomPoint;
        }

        private Vector2 GetRandomDirection()
        {
            return Random.insideUnitCircle.normalized;
        }

        private void NewDestroyedAsteroids(NewDestroyedAsteroidsNotification not)
        {
            for (int i = 0; i < not.NewDestroyedAsteroidDto.Count; i++)
            {
                GetPoolByType(not.NewDestroyedAsteroidDto.Type).Spawn
                (
                    not.NewDestroyedAsteroidDto.Position, 
                    GetRandomDirection(),
                    _gameConfiguration.GetAsteroidConfigurationByType(not.NewDestroyedAsteroidDto.Type)
                );
            }
        }

        private Asteroid.Pool GetPoolByType(AsteroidType type)
        {
            switch (type)
            {
                case AsteroidType.Big:
                    return _bigAsteroidsPool;
                case AsteroidType.Medium:
                    return _mediumAsteroidsPool;
                case AsteroidType.Small:
                    return _smallAsteroidsPool;
            }

            return null;
        }
    }
}