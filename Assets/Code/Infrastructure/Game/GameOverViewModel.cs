using Code.Domain.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.MainMenu;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using Zenject;

namespace Code.Infrastructure.Game
{
    public class GameOverViewModel : IGameOverViewModel
    {
        [Inject] private IPlayer _player;
        [Inject] private IHighScoreService _highScoreService;
        [Inject] private SignalBus _signalBus;
        
        public void NameChangedCommand(string newName)
        {
            _player.SetName(newName);

            if (!IsNameValid())
            {
                _signalBus.Fire<DeactivateReturnToMenuNotification>();
                return;
            }
            
            _signalBus.Fire<ActivateReturnToMenuNotification>();
        }

        private bool IsNameValid()
        {
            return !string.IsNullOrEmpty(_player.Name);
        }

        public async void SaveNewScoreCommand()
        {
            await _highScoreService.NewScore(new HighScoreDto(_player.Name, _player.Score));
            
            _signalBus.Fire<NewScoreSavedNotification>();
        }
    }
}