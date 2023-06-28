using UnityEngine;

namespace TheRat
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
            => transform.position = target.position;
    }
}
