using Code.Infrastructure.Notifications;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.GameOver
{
    public class GameOverMenuView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [Inject] private Camera _camera;
        
        [Inject]
        public void Construct()
        {
            _canvas.worldCamera = _camera;
        }

        public class Factory : PlaceholderFactory<GameOverMenuView>
        {
        }
    }
}