using UnityEngine;

namespace TheRat
{
    public class CursorService : MonoBehaviour
    {
        private Camera mainCamera;

        void Awake()
        {
            mainCamera = Camera.main;
            Cursor.visible = true;
        }
        void LateUpdate()
        {
            Vector3 newPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newPos.x, newPos.y, -100f);
        }

        public void SetCursor(Texture2D cursorTexture) => Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}