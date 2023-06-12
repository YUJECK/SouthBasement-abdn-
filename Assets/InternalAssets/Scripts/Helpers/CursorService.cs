using UnityEngine;

namespace TheRat
{
    public class CursorService : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
            Cursor.visible = true;
        }

        private void LateUpdate()
        {
            var newPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y, -100f);
        }

        public void SetCursor(Texture2D cursorTexture) => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}