using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Utils
{
    public abstract class ButtonHeld : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        protected abstract void OnButtonHeld();

        private bool _heldDown;
        
        private void Update()
        {
            if (_heldDown)
            {
                OnButtonHeld();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _heldDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _heldDown = false;
        }
    }
}