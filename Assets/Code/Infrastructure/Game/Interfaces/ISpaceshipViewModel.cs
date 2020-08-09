using UnityEngine.Events;

namespace Code.Infrastructure.Game.Interfaces
{
    public interface ISpaceshipViewModel
    {
        void ImpulseCommand();
        void RotateLeftCommand();
        void RotateRightCommand();
        void ShootCommand();

        void SpaceShipDamagedCommand();
    }
}