using Code.Presentation.Game.Asteroid;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Infrastructure.Game.Interfaces
{
    public interface IAsteroidsViewModel
    {
        void AsteroidDestroyedCommand(AsteroidType type, Vector3 position, long score);
        void InitializeGalaxyCommand();
    }
}