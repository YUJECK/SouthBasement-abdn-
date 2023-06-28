using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace SouthBasement.CameraHandl
{
    public sealed class CameraHandler
    {
        public Camera MainCamera { get; private set; }
        
        private Dictionary<CameraNames, CinemachineVirtualCamera> _cameras;
        private PixelPerfectCamera _pixelPerfectCamera;

        public CameraNames CurrentCamera { get; private set; }

        public CameraHandler(Dictionary<CameraNames, CinemachineVirtualCamera> cameras, PixelPerfectCamera pixelPerfectCamera, Camera mainCamera)
        {
            _cameras = cameras;
            _pixelPerfectCamera = pixelPerfectCamera;
            MainCamera = mainCamera;

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
            _cameras[CurrentCamera].gameObject.SetActive(false);
            _cameras[name].gameObject.SetActive(true);
            
            CurrentCamera = name;
        }
    }
}