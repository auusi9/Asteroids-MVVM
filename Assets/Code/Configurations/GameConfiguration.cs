using System;
using Code.Presentation.Game.Asteroid;
using UnityEngine;

namespace Code.Configurations
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "Configurations/GameConfiguration", order = 1)]
    public class GameConfiguration : ScriptableObject
    {
        [SerializeField] private EnemyConfiguration _bigAsteroidConfiguration;
        [SerializeField] private EnemyConfiguration _mediumAsteroidConfiguration;
        [SerializeField] private EnemyConfiguration _smallAsteroidConfiguration;
        [SerializeField] private SaucerConfiguration _bigSaucerConfiguration;
        [SerializeField] private SaucerConfiguration _smallSaucerConfiguration;
        [SerializeField] private float _asteroidSpawnDistance = 5;

        public EnemyConfiguration BigAsteroidConfiguration => _bigAsteroidConfiguration;

        public EnemyConfiguration MediumAsteroidConfiguration => _mediumAsteroidConfiguration;

        public EnemyConfiguration SmallAsteroidConfiguration => _smallAsteroidConfiguration;

        public SaucerConfiguration BigSaucerConfiguration => _bigSaucerConfiguration;

        public SaucerConfiguration SmallSaucerConfiguration => _smallSaucerConfiguration;

        public float AsteroidSpawnDistance => _asteroidSpawnDistance;


        public EnemyConfiguration GetAsteroidConfigurationByType(AsteroidType type)
        {
            switch (type)
            {
                case AsteroidType.Big:
                    return BigAsteroidConfiguration;
                case AsteroidType.Medium:
                    return MediumAsteroidConfiguration;
                case AsteroidType.Small:
                    return SmallAsteroidConfiguration;
            }

            return null;
        }
    }

    [Serializable]
    public class EnemyConfiguration
    {
        [SerializeField] private long _destroyScore;
        [SerializeField] private float _velocity;

        public long DestroyScore => _destroyScore;

        public float Velocity => _velocity;
    } 
    
    [Serializable]
    public class SaucerConfiguration : EnemyConfiguration
    {
        [SerializeField] private float _shootInterval;
        [SerializeField] private float _projectileVelocity;

        public float ShootInterval => _shootInterval;
        public float ProjectileVelocity => _projectileVelocity;
    }
}