using UnityEngine;

namespace SouthBasement
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
            => transform.position = target.position;
    }
}
