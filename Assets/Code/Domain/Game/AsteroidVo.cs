using Code.Presentation.Game.Asteroid;
using UnityEngine;

namespace Code.Domain.Game
{
    public class AsteroidVo
    {
        public AsteroidType Type { get; }

        public AsteroidVo(AsteroidType type)
        {
            Type = type;
        }
    }
}