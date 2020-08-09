using Code.Utils;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Code.Presentation.Game
{
    public class KeepInsideCameraView : MonoBehaviour
    {
        [Inject] private Camera _camera;

        public event UnityAction Teleported;

        private void Update()
        {
            Vector2 bounds = _camera.WorldBoundsSize();

            if (transform.position.x < -bounds.x)
            {
                transform.position = new Vector3(bounds.x, transform.position.y, transform.position.z);
                InvokeTeleportEvent();
            }

            if (transform.position.x > bounds.x)
            {
                transform.position = new Vector3(-bounds.x, transform.position.y, transform.position.z); 
                InvokeTeleportEvent();
            }  
            
            if (transform.position.y < -bounds.y)
            {
                transform.position = new Vector3(transform.position.x, bounds.y, transform.position.z);
                InvokeTeleportEvent();
            }

            if (transform.position.y > bounds.y)
            {
                transform.position = new Vector3(transform.position.x, -bounds.y, transform.position.z);
                InvokeTeleportEvent();
            }
        }

        private bool IsInsideCamera(Bounds bounds)
        {
            return bounds.Contains(transform.position);
        }

        private void InvokeTeleportEvent()
        {
            Teleported?.Invoke();
        }
    }
}