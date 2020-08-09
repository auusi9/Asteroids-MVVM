using Code.Infrastructure.MainMenu;
using Code.Presentation.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject HighScoreElement;
        
        public override void InstallBindings()
        {
            Container.BindFactory<int, HighScoreDto, Transform, HighScoreElement, HighScoreElement.Factory>().FromComponentInNewPrefab(HighScoreElement);
        }
    }
}