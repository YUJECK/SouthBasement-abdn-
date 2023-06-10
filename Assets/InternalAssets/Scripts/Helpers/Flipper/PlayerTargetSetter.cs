using TheRat.Player;
using UnityEngine;

namespace TheRat.PlayerServices
{
    [RequireComponent(typeof(Flipper))]
    public sealed class PlayerTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Flipper>().Target = FindObjectOfType<Character>().transform;
        }
    }
}