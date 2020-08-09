using Code.Presentation.Game.Saucer;
using UnityEngine.Events;

namespace Code.Infrastructure.Game.Interfaces
{
    public interface ISaucersViewModel
    {
        void SaucerDestroyedCommand(SaucerType type, long score);
    }
}