using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cinemachine;
using SouthBasement.Extensions.DataStructures;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace SouthBasement.CameraHandl
{
    public sealed class CamerasContainer : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<CameraTags, CinemachineVirtualCamera> _virtualCameras;
        private PixelPerfectCamera _pixelPerfectCamera;
        
        public Dictionary<CameraTags, CinemachineVirtualCamera> GetCameras() => _virtualCameras.ToDictionary();
        public PixelPerfectCamera GetPixelPerfectCamera() => GetComponentInChildren<PixelPerfectCamera>();

        public Camera GetMainCamera() => GetComponentInChildren<Camera>();
    }
}