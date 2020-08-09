using Code.Infrastructure.Game;
using Code.Infrastructure.Notifications;
using Code.Presentation.UI.Game;
using Code.Presentation.UI.GameOver;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class InGameUIInstaller : MonoInstaller
    {
        [SerializeField] private Transform LivesTransform;
        public GameObject LifeUIPrefab;
        public GameObject GameOverUI;
        
        public override void InstallBindings()
        {
            DeclareSignals();
            BindPools();
            BindViewModel();
            BindFactories();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<ActivateReturnToMenuNotification>().OptionalSubscriber();
            Container.DeclareSignal<DeactivateReturnToMenuNotification>().OptionalSubscriber();
            Container.DeclareSignal<NewScoreSavedNotification>().OptionalSubscriber();
        }
        
        private void BindPools()
        {
            Container.BindMemoryPool<LifeView, LifeView.Pool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(LifeUIPrefab)
                .UnderTransform(LivesTransform);
        }
        
        private void BindViewModel()
        {
            Container.BindInterfacesTo<GameOverViewModel>().AsSingle();
        }
             
        private void BindFactories()
        {
            Container.BindFactory<GameOverMenuView, GameOverMenuView.Factory>().FromComponentInNewPrefab(GameOverUI);
        }
    }
}