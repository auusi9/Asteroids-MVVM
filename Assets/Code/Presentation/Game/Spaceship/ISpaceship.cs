using UnityEngine;

namespace Code.Presentation.Game.Spaceship
{
    public interface ISpaceship : IImpulse, IRotate, IShoot, IPosition, IInmortal
    {
    }
    
    public interface IImpulse
    {
        void Impulse();
    }
    
    public interface IRotate
    {
        void RotateLeft();
        void RotateRight();
    }
    public interface IShoot
    {
        void Shoot();
    }

    public interface IInmortal
    {
        bool Inmortal { get; }
    }

    public interface IPosition
    {
        Vector3 Position { get; }
    }
}