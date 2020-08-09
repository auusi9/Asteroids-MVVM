using System;
using Code.Domain.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using Code.Presentation.Game.Saucer;
using UnityEngine.Events;
using Zenject;

namespace Code.Infrastructure.Game
{
    public class SaucersViewModel : ISaucersViewModel, IInitializable, IDisposable
    {
        [Inject] private IGalaxy _galaxy;
        [Inject] private IPlayer _player;
        [Inject] private ISaucersAppearanceService _saucersAppearanceService;
        [Inject] private SignalBus _signalBus;

        public void SaucerDestroyedCommand(SaucerType type, long score)
        {
            _player.AddScore(score);
            _signalBus.Fire(new SpaceshipLivesAmountNotification(_player.Life));
            _signalBus.Fire(new ScoreUpdatedNotification(_player.Score));
        }
        
        private void RequestSaucer(object sender, EventArgs e)
        {
            _signalBus.Fire(new NewSaucerNotification(_galaxy.NewSaucer()));
        }

        public void Initialize()
        {
            _saucersAppearanceService.NewSaucer += RequestSaucer;
        }

        public void Dispose()
        {
            _saucersAppearanceService.NewSaucer -= RequestSaucer;
        }
    }
}