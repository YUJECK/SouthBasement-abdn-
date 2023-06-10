    using UnityEngine;

namespace TheRat.PlayerServices
{
    [RequireComponent(typeof(Flipper))]
    public sealed class MouserTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Flipper>().Target = FindObjectOfType<Mouse>().transform;
        }
    }
}