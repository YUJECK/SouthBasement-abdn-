using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace SouthBasement.CameraHandl
{
    public sealed class CameraHandler : IMainCameraContainer
    {
        public Camera MainCamera { get; private set; }
        public Camera MapCamera { get; private set; }
        
        private readonly Dictionary<CameraNames, CinemachineVirtualCamera> _cameras;
        private PixelPerfectCamera _pixelPerfectCamera;

        public CameraNames CurrentCamera { get; private set; }

        public CameraHandler(CamerasContainer camerasContainer)
        {
            _cameras = camerasContainer.GetCameras();
            _pixelPerfectCamera = camerasContainer.GetPixelPerfectCamera();
            MainCamera = camerasContainer.GetMainCamera();
            MapCamera = camerasContainer.GetMapCamera();

            DisableAll();
            SwitchTo(CameraNames.Main);
        }

        private void DisableAll()
        {
            foreach (var cameraPair in _cameras)
                cameraPair.Value.gameObject.SetActive(false);
        }

        public void SwitchTo(CameraNames name)
        {
            if(_cameras[name].isActiveAndEnabled)
                return;
                
            _cameras[CurrentCamera].gameObject.SetActive(false);
            _cameras[name].gameObject.SetActive(true);
            
            CurrentCamera = name;
        }

        public void SwitchToNPCCamera(Transform target)
        {
            SwitchTo(CameraNames.NPC);
            _cameras[CurrentCamera].Follow = target;
        }
    }
}