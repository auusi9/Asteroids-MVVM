using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

namespace Code.Presentation.UI.MainMenu
{
    public class PlayButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(LoadGameScene);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadGameScene);
        }

        private void LoadGameScene()
        {
            Addressables.LoadSceneAsync("MainScene");
        }
    }
}