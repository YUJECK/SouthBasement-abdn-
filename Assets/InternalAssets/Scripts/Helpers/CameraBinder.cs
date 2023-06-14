using Cinemachine;
using TheRat.Characters;
using UnityEngine;
using Zenject;

namespace TheRat
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraBinder : MonoBehaviour
    {
        [Inject]
        private void Construct(Character character)
            => GetComponent<CinemachineVirtualCamera>().Follow = character.transform;
    }
}
