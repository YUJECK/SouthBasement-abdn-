using UnityEngine;

namespace TheRat
{
    public class Mouse : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
