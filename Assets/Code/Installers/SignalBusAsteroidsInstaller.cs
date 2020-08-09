using Zenject;

namespace Code.Installers
{
    public class SignalBusAsteroidsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}