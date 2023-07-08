using UnityEngine;

namespace SouthBasement.CameraHandl
{
    public sealed class MainCameraContainer : IMainCameraContainer
    {
        public Camera MainCamera { get; private set; }

        public MainCameraContainer(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }
    }
}