using Code.Presentation.Game.Asteroid;
using Code.Presentation.Game.Saucer;
using UnityEngine;

namespace Code.Domain.Game
{
    public interface IGalaxy
    {
        AsteroidVo[] NewGalaxy(int numberOfAsteroids);
        AsteroidVo[] AsteroidDestroyed(AsteroidType asteroidType, long score);

        SaucerType NewSaucer();
        
        int NumberOfAsteroids { get; }
        int Level { get; }
    }
}