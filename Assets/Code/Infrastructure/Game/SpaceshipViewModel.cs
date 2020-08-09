using Code.Domain.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using Code.Infrastructure.Services;
using UnityEngine.Events;
using Zenject;

namespace Code.Infrastructure.Game
{
    public class SpaceshipViewModel : ISpaceshipViewModel
    {
        [Inject] private IPlayer _player;
        [Inject] private ISaucersAppearanceService _saucersAppearanceService;
        [Inject] private SignalBus _signalBus;
        public void ImpulseCommand()
        {
            _signalBus.Fire<ImpulseSpaceshipNotification>();
        }

        public void RotateLeftCommand()
        {
            _signalBus.Fire<RotateSpaceshipLeftNotification>();
        }

        public void RotateRightCommand()
        {
            _signalBus.Fire<RotateSpaceshipRightNotification>();
        }

        public void ShootCommand()
        {
            _signalBus.Fire<SpaceshipShootNotification>();
        }
        
        public void SpaceShipDamagedCommand()
        {
            _player.SpaceshipDamaged();
            
            _signalBus.Fire(new SpaceshipLivesAmountNotification(_player.Life));

            if (IsPlayerAlive())
            {
                _signalBus.Fire<RespawnSpaceshipNotification>();
                return;
            }
            _saucersAppearanceService.GameOver();
            _signalBus.Fire<SpaceshipDiedNotification>();
        }

        private bool IsPlayerAlive()
        {
            return _player.Life > 0;
        }
    }
}