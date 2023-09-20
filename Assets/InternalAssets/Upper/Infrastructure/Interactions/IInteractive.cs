using System;

namespace SouthBasement.Interactions
{
    public interface IInteractive
    {
        event Action<IInteractive> OnDetected;
        event Action<IInteractive> OnInteracted;
        event Action<IInteractive> OnDetectionReleased;
        
        public void Detect();
        public void Interact();
        public void DetectionReleased();
    }
}