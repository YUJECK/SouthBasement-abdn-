using System;
using UnityEngine;

namespace SouthBasement.Interactions
{
    public abstract class InteractiveGameObject : MonoBehaviour, IInteractive
    {
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;
        
        public void Detect()
        {
            OnDetected?.Invoke(this);
            OnInteractionDetected();
        }

        public void Interact()
        {
            OnInteracted?.Invoke(this);
            OnInteractionInteracted();
        }

        public void DetectionReleased()
        {
            OnInteracted?.Invoke(this);
            OnInteractionDetectedReleased();
        }

        public virtual void OnInteractionDetected() { }
        public virtual void OnInteractionInteracted() { }
        public virtual void OnInteractionDetectedReleased() { }
    }
}