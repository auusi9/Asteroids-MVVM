using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Presentation.UI
{
    public class SplashView : MonoBehaviour
    {
        private void Start()
        {
            Addressables.LoadSceneAsync("MainMenu");
        }
    }
}