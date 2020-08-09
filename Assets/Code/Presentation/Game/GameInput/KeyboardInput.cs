using Code.Infrastructure.Game;
using Code.Infrastructure.Game.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Presentation.Game.GameInput
{
    public class KeyboardInput : MonoBehaviour
    {
        [Inject] private ISpaceshipViewModel _spaceshipViewModel;
        
        private void Update()
        {
            if (Input.GetAxis("Vertical") > 0.2f)
            {
                _spaceshipViewModel.ImpulseCommand();
            }

            if (Input.GetAxis("Horizontal") > 0.2f)
            {
                _spaceshipViewModel.RotateRightCommand();
            }
        
            if (Input.GetAxis("Horizontal") < -0.2f)
            {
                _spaceshipViewModel.RotateLeftCommand();
            }
        
            if (Input.GetButtonDown("Jump"))
            {
                _spaceshipViewModel.ShootCommand();
            }
        }
    }
}