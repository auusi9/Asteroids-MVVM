using Code.Infrastructure.Game.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class ShootButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;

        private void Start()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            _spaceshipViewModel.ShootCommand();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ButtonClicked);
        }
    }
}