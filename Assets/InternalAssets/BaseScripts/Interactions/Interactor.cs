using System.Collections.Generic;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement.Interactions
{
    public sealed class Interactor : MonoBehaviour
    {
        private readonly List<IInteractive> _availableInteractions = new();

        [Inject]
        private void Construct(IInputService inputService)
        {
            inputService.OnInteracted += Interact;
        }
        
        private void Interact()
        {
            IInteractive[] interactives = _availableInteractions.ToArray();
            
            foreach (var interactive in interactives)
                interactive?.Interact();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IInteractive interactive))
            {
                interactive.Detect();
                _availableInteractions.Add(interactive);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IInteractive interactive))
            {
                if (_availableInteractions.Contains(interactive))
                {
                    interactive.DetectionReleased();
                    _availableInteractions.Remove(interactive);
                }
            }
        }
    }
}