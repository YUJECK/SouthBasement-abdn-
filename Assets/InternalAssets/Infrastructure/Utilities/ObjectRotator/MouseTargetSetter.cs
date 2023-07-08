using System;
using UnityEngine;
using Zenject;

namespace SouthBasement.Helpers.Rotator
{
    [RequireComponent(typeof(ObjectRotator))]
    public sealed class MouseTargetSetter : MonoBehaviour
    {
        private CursorService _cursorService;

        [Inject]
        private void Construct(CursorService cursorService)
        {
            _cursorService = cursorService;
        }
        
        private void Awake()
        {
            GetComponent<ObjectRotator>().Target = transform;
        }

        private void Update()
        {
            transform.position = _cursorService.CursorPosition;
        }
    }
}