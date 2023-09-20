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
        [SerializeField] private SerializedDictionary<CameraNames, CinemachineVirtualCamera> _virtualCameras;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera mapCamera;
        
        private PixelPerfectCamera _pixelPerfectCamera;
        
        public Dictionary<CameraNames, CinemachineVirtualCamera> GetCameras() => _virtualCameras.ToDictionary();
        public PixelPerfectCamera GetPixelPerfectCamera() => GetComponentInChildren<PixelPerfectCamera>();

        public Camera GetMainCamera() => mainCamera;
        public Camera GetMapCamera() => mapCamera;
    }
}