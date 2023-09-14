using System;
using SouthBasement.Interactions;
using UnityEngine;

namespace SouthBasement
{
    public sealed class RubbishDealerInteractive : MonoBehaviour, IInteractive
    {
        public RubbishDealerUIController UIController;
        
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;

        public void Detect()
        {
            UIController.EnableUI();
        }

        public void Interact() { }

        public void DetectionReleased()
        {
            UIController.DisableUI();
        }
    }
}