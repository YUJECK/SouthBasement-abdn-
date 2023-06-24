using TheRat.CameraHandl;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public class CursorService : MonoBehaviour
    {
        private Camera _mainCamera;

        [Inject]
        private void Construct(CameraHandler cameraHandler)
        {
            _mainCamera = cameraHandler.MainCamera;
        }
        
        private void LateUpdate()
        {
            var newPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y, -100f);
        }

        public void SetCursor(Texture2D cursorTexture) => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}