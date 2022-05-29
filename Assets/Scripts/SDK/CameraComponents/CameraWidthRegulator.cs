using UnityEngine;

namespace SDK.CameraComponents
{
    public class CameraWidthRegulator :ICameraWidthRegulation
    {
        private Camera _camera;

        public CameraWidthRegulator(Camera camera)
        {
            _camera = camera;
        }

        public void SetWidth(float width)
        {
            width /= 2;
            var cameraSize = width / _camera.aspect;
            _camera.orthographicSize = cameraSize;
        }
    }
}