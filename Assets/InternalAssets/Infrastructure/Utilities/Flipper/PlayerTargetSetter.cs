using SouthBasement.Characters.Base;
using UnityEngine;

namespace SouthBasement.PlayerServices
{
    [RequireComponent(typeof(Flipper))]
    public sealed class PlayerTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Flipper>().Target = FindObjectOfType<CharacterGameObject>().transform;
        }
    }
}