using Code.Presentation.Game.Asteroid;
using UnityEngine;

namespace Code.Infrastructure.Game
{
    public class NewAsteroidsDto
    {
        public int Count { get; }
        public AsteroidType Type { get; }

        public NewAsteroidsDto(int count, AsteroidType type)
        {
            Count = count;
            Type = type;
        }
    }

    public class NewDestroyedAsteroidDto : NewAsteroidsDto
    {
        public Vector3 Position { get; }

        public NewDestroyedAsteroidDto(int count, AsteroidType type, Vector3 position) : base(count, type)
        {
            Position = position;
        }
    }
}