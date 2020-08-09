using System;
using System.Linq;
using Code.Domain.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using Code.Presentation.Game.Asteroid;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Code.Infrastructure.Game
{
    public class AsteroidsViewModel : IAsteroidsViewModel
    {
        [Inject] private IPlayer _player;
        [Inject] private IGalaxy _galaxy;
        [Inject] private ISaucersAppearanceService _saucersAppearanceService;
        [Inject] private IAsteroidsPerLevelService _asteroidsPerLevelService;
        [Inject] private SignalBus _signalBus;

        public void InitializeGalaxyCommand()
        {
            int asteroidsNum = _asteroidsPerLevelService.Get(_galaxy.Level);
            AsteroidVo[] asteroidVo = _galaxy.NewGalaxy(asteroidsNum);
            
            _saucersAppearanceService.StartLevel();
            
            _signalBus.Fire(new SpaceshipLivesAmountNotification(_player.Life));
            _signalBus.Fire(new NewAsteroidsNotification(new NewAsteroidsDto(asteroidVo.Length, AsteroidType.Big)));
        }

        public void AsteroidDestroyedCommand(AsteroidType type, Vector3 position, long score)
        {
            AsteroidVo[] asteroidVos = null;
            try
            {
                asteroidVos = _galaxy.AsteroidDestroyed(type, score);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }
            _signalBus.Fire(new SpaceshipLivesAmountNotification(_player.Life));
            _signalBus.Fire(new ScoreUpdatedNotification(_player.Score));

            if (HasCreatedNewAsteroids(asteroidVos))
            {
                _signalBus.Fire(new NewDestroyedAsteroidsNotification(GetNewAsteroidsDtoByType(asteroidVos, AsteroidType.Medium, position)));
                _signalBus.Fire(new NewDestroyedAsteroidsNotification(GetNewAsteroidsDtoByType(asteroidVos, AsteroidType.Small, position)));
            }

            if (IsLevelFinished())
            {
                _saucersAppearanceService.EndLevel();
                InitializeGalaxyCommand();
            }
        }

        private static bool HasCreatedNewAsteroids(AsteroidVo[] asteroidVos)
        {
            return asteroidVos != null && asteroidVos.Length > 0;
        }

        private bool IsLevelFinished()
        {
            return _galaxy.NumberOfAsteroids == 0;
        }

        private NewDestroyedAsteroidDto GetNewAsteroidsDtoByType(AsteroidVo[] asteroidVos, AsteroidType type, Vector3 position)
        {
            int count = asteroidVos.Count(x => x.Type == type);
            
            return new NewDestroyedAsteroidDto(count, type, position);
        }
    }
}