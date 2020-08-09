using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Code.Presentation.UI.GameOver
{
    public class BackToMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [Inject] private SignalBus _signalBus;
        [Inject] private IGameOverViewModel _gameOverViewModel;

        private void Start()
        {
            DeactivateButton();
            _signalBus.Subscribe<DeactivateReturnToMenuNotification>(DeactivateButton);
            _signalBus.Subscribe<ActivateReturnToMenuNotification>(ActivateButton);
            _signalBus.Subscribe<NewScoreSavedNotification>(GoToScene);
            _button.onClick.AddListener(SaveNewScore);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(SaveNewScore);
            _signalBus.TryUnsubscribe<DeactivateReturnToMenuNotification>(DeactivateButton);
            _signalBus.TryUnsubscribe<ActivateReturnToMenuNotification>(ActivateButton);
            _signalBus.TryUnsubscribe<NewScoreSavedNotification>(GoToScene);

        }

        private void SaveNewScore()
        {
            _gameOverViewModel.SaveNewScoreCommand();
        }
        
        private void GoToScene()
        {
            Addressables.LoadSceneAsync("MainMenu");
        }

        private void ActivateButton()
        {
            _button.interactable = true;
        }

        private void DeactivateButton()
        {
            _button.interactable = false;
        }
    }
}