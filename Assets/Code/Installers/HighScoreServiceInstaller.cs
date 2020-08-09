using Code.Infrastructure.Services;
using Zenject;

namespace Code.Installers
{
    public class HighScoreServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<HighScoreService>().AsTransient();
        }
    }
}