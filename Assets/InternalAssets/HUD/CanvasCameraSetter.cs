using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasCameraSetter : MonoBehaviour
    {
        private void Start()
            => GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
