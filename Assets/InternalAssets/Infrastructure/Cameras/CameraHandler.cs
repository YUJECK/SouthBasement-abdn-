using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace SouthBasement.CameraHandl
{
    public sealed class CameraHandler
    {
        public Camera MainCamera { get; private set; }
        
        private Dictionary<CameraTags, CinemachineVirtualCamera> _cameras;
        private PixelPerfectCamera _pixelPerfectCamera;

        public CameraTags CurrentCamera { get; private set; }

        public CameraHandler(Dictionary<CameraTags, CinemachineVirtualCamera> cameras, PixelPerfectCamera pixelPerfectCamera, Camera mainCamera)
        {
            _cameras = cameras;
            _pixelPerfectCamera = pixelPerfectCamera;
            MainCamera = mainCamera;

            DisableAll();
            SwitchTo(CameraTags.Main);
        }

        private void DisableAll()
        {
            foreach (var cameraPair in _cameras)
                cameraPair.Value.gameObject.SetActive(false);
        }

        public void SwitchTo(CameraTags tag)
        {
            _cameras[CurrentCamera].gameObject.SetActive(false);
            _cameras[tag].gameObject.SetActive(true);
            
            CurrentCamera = tag;
        }
    }
}