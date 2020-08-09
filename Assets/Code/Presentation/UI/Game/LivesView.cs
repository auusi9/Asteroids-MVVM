using System.Collections.Generic;
using Code.Infrastructure.Notifications;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class LivesView : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private LifeView.Pool _pool;

        private List<LifeView> _activeLives = new List<LifeView>();

        private void Start()
        {
            _signalBus.Subscribe<SpaceshipLivesAmountNotification>(LivesAmount);
        } 
        
        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<SpaceshipLivesAmountNotification>(LivesAmount);
        }

        private void LivesAmount(SpaceshipLivesAmountNotification not)
        {
            long missingLives = not.Lives - _activeLives.Count;

            if (missingLives > 0)
            {
                for (int i = 0; i < missingLives; i++)
                {
                    LifeView lifeView = _pool.Spawn();
                    _activeLives.Add(lifeView);
                }
            }
            else if(missingLives < 0 && _activeLives.Count > 0)
            {
                for (int i = 0; i < Mathf.Abs(missingLives); i++)
                {
                    _pool.Despawn(_activeLives[_activeLives.Count - 1]);
                    _activeLives.RemoveAt(_activeLives.Count - 1);
                }
            }
        }
    }
}