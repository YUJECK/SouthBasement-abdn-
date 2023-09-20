using UnityEngine;
using Zenject;

namespace SouthBasement.Helpers.Rotator
{
    [RequireComponent(typeof(ObjectRotator))]
    public sealed class MouseTargetSetter : MonoBehaviour
    {
        [SerializeField] private Transform cursorPosition;
        private CursorService _cursorService;

        [Inject]
        private void Construct(CursorService cursorService) => _cursorService = cursorService;

        private void Awake() => GetComponent<ObjectRotator>().Target = cursorPosition;

        private void Update() => cursorPosition.position = _cursorService.CursorPosition;
    }
}