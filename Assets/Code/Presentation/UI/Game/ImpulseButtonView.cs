using Code.Infrastructure.Game.Interfaces;
using Code.Utils;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class ImpulseButtonView : ButtonHeld
    {
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;
        
        protected override void OnButtonHeld()
        {
            _spaceshipViewModel.ImpulseCommand();
        }
    }
}