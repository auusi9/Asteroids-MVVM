using Code.Infrastructure.Game.Interfaces;
using Code.Utils;
using Zenject;

namespace Code.Presentation.UI.Game
{
    public class RotateLeftButtonView : ButtonHeld
    {        
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;

        protected override void OnButtonHeld()
        {
            _spaceshipViewModel.RotateLeftCommand();
        }
    }
}