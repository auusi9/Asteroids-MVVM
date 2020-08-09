using UnityEngine;

namespace Code.Utils
{
    public static class CameraExtensions
    {
        public static Vector2 WorldBoundsSize(this Camera camera)
        {
            return camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
        }
    }
}