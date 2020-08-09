using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.Saucer
{
    public class SaucersView : MonoBehaviour
    {
        [SerializeField] private GameObject[] _rightSpawnPoints;
        [SerializeField] private GameObject[] _leftSpawnPoints;
        [Inject(Id = SaucerType.Big)] private BigSaucer.Pool _bigSaucerPool;
        [Inject(Id = SaucerType.Small)] private SmallSaucer.Pool _smallSaucerPool;
        [Inject] private ISaucersViewModel _saucersViewModel;
        [Inject] private SignalBus _signalBus;

        private void Start()
        {
            _signalBus.Subscribe<NewSaucerNotification>(CreateNewSaucer);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<NewSaucerNotification>(CreateNewSaucer);
        }
        
        private void CreateNewSaucer(NewSaucerNotification saucerNot)
        {
            switch (saucerNot.SaucerType)
            {
                case SaucerType.Big:
                    _bigSaucerPool.Spawn(GetRandomSaucerConfig());
                    break;
                case SaucerType.Small:
                    _smallSaucerPool.Spawn(GetRandomSaucerConfig());
                    break;
            }
        }

        private SaucerConfig GetRandomSaucerConfig()
        {
            if (Random.Range(0, 1) == 0)
            {
                return CreateFromLeftToRight();
            }

            return CreateFromRightToLeft();
        }

        private SaucerConfig CreateFromRightToLeft()
        {
            SaucerConfig config;
            config.Destination = GetRandomLeftSpawnPoint();
            config.Origin = GetRandomRightSpawnPoint();

            return config;
        }
               
        private SaucerConfig CreateFromLeftToRight()
        {
            SaucerConfig config;
            config.Destination = GetRandomLeftSpawnPoint();
            config.Origin = GetRandomRightSpawnPoint();

            return config;
        }

        private Vector3 GetRandomRightSpawnPoint()
        {
            return _rightSpawnPoints[Random.Range(0, _rightSpawnPoints.Length)].transform.position;
        }
        
        private Vector3 GetRandomLeftSpawnPoint()
        {
            return _leftSpawnPoints[Random.Range(0, _leftSpawnPoints.Length)].transform.position;
        }

        public void NewDestination(Saucer saucer)
        {
            saucer.Initialize(GetRandomSaucerConfig());
        }
    }

    public struct SaucerConfig
    {
        public Vector3 Destination;
        public Vector3 Origin;
    }
}