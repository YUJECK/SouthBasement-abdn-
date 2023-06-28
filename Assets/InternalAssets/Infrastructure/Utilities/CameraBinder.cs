using Cinemachine;
using SouthBasement.Characters;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraBinder : MonoBehaviour
    {
        [Inject]
        private void Construct(Character character)
            => GetComponent<CinemachineVirtualCamera>().Follow = character.transform;
    }
}
