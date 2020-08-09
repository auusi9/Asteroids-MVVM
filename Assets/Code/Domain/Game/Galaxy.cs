using System;
using System.Collections.Generic;
using System.Linq;
using Code.Presentation.Game.Asteroid;
using Code.Presentation.Game.Saucer;
using Zenject;

namespace Code.Domain.Game
{
    public class Galaxy : IGalaxy
    {
        [Inject] private IPlayer _player;
        [Inject] private IRandomSaucerHelper _randomSaucer;

        public int NumberOfAsteroids => _asteroidsOnGalaxy.Count;
        public int Level { get; private set; }
        
        private readonly List<AsteroidVo> _asteroidsOnGalaxy = new List<AsteroidVo>();

        public AsteroidVo[] NewGalaxy(int numberOfAsteroids)
        {
            AsteroidVo[] asteroidVos = new AsteroidVo[numberOfAsteroids];

            for (int i = 0; i < numberOfAsteroids; i++)
            {
                asteroidVos[i] = new AsteroidVo(AsteroidType.Big);
            }
            _asteroidsOnGalaxy.AddRange(asteroidVos);
            return asteroidVos;
        }

        public AsteroidVo[] AsteroidDestroyed(AsteroidType asteroidType, long score)
        {
            AsteroidVo asteroidVo = _asteroidsOnGalaxy.FirstOrDefault(x => x.Type == asteroidType);

            if (asteroidVo == null)
            {
                throw new Exception("Asteroid with type: " + asteroidType.ToString() + " not found on galaxy");
            }

            _asteroidsOnGalaxy.Remove(asteroidVo);

            AsteroidVo[] newAsteroids = null;
            _player.AddScore(score);

            switch (asteroidType)
            {
                case AsteroidType.Big:
                    newAsteroids = CreateAsteroids(AsteroidType.Medium, 2);
                    _asteroidsOnGalaxy.AddRange(newAsteroids);
                    break;
                case AsteroidType.Medium:
                    newAsteroids = CreateAsteroids(AsteroidType.Small, 2);
                    _asteroidsOnGalaxy.AddRange(newAsteroids);
                    break;
                case AsteroidType.Small:
                    newAsteroids = new AsteroidVo[0];
                    break;
            }

            if (NumberOfAsteroids == 0)
            {
                Level++;
            }
                        
            return newAsteroids;
        }

        private AsteroidVo[] CreateAsteroids(AsteroidType type, int numberOfNewAsteroids)
        {
            AsteroidVo[] asteroidVos = new AsteroidVo[numberOfNewAsteroids];

            for (int i = 0; i < numberOfNewAsteroids; i++)
            {
                asteroidVos[i] = new AsteroidVo(type);
            }

            return asteroidVos;
        }
        
        public SaucerType NewSaucer()
        {
            if (_player.Score >= 40000)
            {
                return SaucerType.Small;
            }

            return _randomSaucer.GetRandomSaucer();
        }
    }
}