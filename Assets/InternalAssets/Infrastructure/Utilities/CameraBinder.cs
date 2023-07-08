using Cinemachine;
using SouthBasement.Characters;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraBinder : MonoBehaviour
    {
        private void Awake()
            => GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<Character>().transform;
    }
}