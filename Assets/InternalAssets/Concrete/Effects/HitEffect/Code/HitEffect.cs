using UnityEngine;

namespace SouthBasement.Effects
{
    public sealed class HitEffect : MonoBehaviour
    {
        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}