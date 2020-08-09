using Code.Infrastructure.Game.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Presentation.UI.GameOver
{
    public class PlayerInputNameView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _tmpInputField;
        [Inject] private IGameOverViewModel _gameOverViewModel;

        private void Start()
        {
            _tmpInputField.onValueChanged.AddListener(TextValueChanged);
        }

        private void OnDestroy()
        {
            _tmpInputField.onValueChanged.RemoveListener(TextValueChanged);
        }

        private void TextValueChanged(string arg0)
        {
            _gameOverViewModel.NameChangedCommand(arg0);
        }
    }
}