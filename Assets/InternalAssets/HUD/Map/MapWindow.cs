using SouthBasement.CameraHandl;
using SouthBasement.HUD.Base;
using SouthBasement.InputServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD.Map
{
    public sealed class MapWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private GameObject map;

        public bool CurrentlyOpened { get; private set; }
        
        private IInputService _inputService;
        private RenderTexture renderTexture;
        private CameraHandler _cameraHandler;

        [Inject]
        private void Construct(IInputService inputService, CameraHandler cameraHandler)
        {
            _inputService = inputService;
            _cameraHandler = cameraHandler;
        }

        private void Start()
        {
            // Получаем размер экрана
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            // Создаем Render Texture с размером экрана
            renderTexture = new RenderTexture(screenWidth, screenHeight, 24);
    
            // Устанавливаем Render Texture как цель камеры
            _cameraHandler.MapCamera.targetTexture = renderTexture;
    
            // Устанавливаем размер текстуры в UI RawImage
            RawImage rawImage = GetComponentInChildren<RawImage>();
            rawImage.texture = renderTexture;
            rawImage.rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
        }

        private void Awake()
        {
            _inputService.OnMapOpen += Open;
            _inputService.OnMapClosed += Close;
        }

        public void Open()
        {
            map.SetActive(true);
            CurrentlyOpened = true;
        }

        public void Close()
        {
            map.SetActive(false);
            CurrentlyOpened = false;
        }

        public void UpdateWindow()
        {
            
        }
    }
}