using Code.Infrastructure.Game.Interfaces;
using Code.Infrastructure.Notifications;
using UnityEngine;
using TMPro;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshProUGui;

        [Inject] private IAsteroidsViewModel _asteroidsViewModel;
        [Inject] private SignalBus _signalBus;

        private void Start()
        {
            _signalBus.Subscribe<ScoreUpdatedNotification>(UpdateScore);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<ScoreUpdatedNotification>(UpdateScore);
        }

        private void UpdateScore(ScoreUpdatedNotification not)
        {
            _textMeshProUGui.SetText(not.Score.ToString());
        }
    }
}