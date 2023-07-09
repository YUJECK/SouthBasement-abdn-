using SouthBasement.CameraHandl;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class CursorService : ITickable 
    {
        private Camera _mainCamera;

        public Vector2 CursorPosition { get; private set; }

        // [Inject]
        // private void Construct(IMainCameraContainer cameraHandler) 
        //     => _mainCamera = cameraHandler.MainCamera;
        
        public void Tick()
        {
            var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CursorPosition = new Vector3(newPos.x, newPos.y, -100f);
        }

        public void SetCursor(Texture2D cursorTexture) => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}