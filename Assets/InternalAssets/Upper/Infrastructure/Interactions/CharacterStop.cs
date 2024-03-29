﻿using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using UnityEngine;
using Zenject;

namespace SouthBasement.Interactions
{
    public sealed class CharacterStop : MonoBehaviour, IInteractive
    {
        public event Action<IInteractive> OnDetected;
        public event Action<IInteractive> OnInteracted;
        public event Action<IInteractive> OnDetectionReleased;
        
        private Character _character;

        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public void Detect()
            => OnDetected?.Invoke(this);

        public void Interact()
            => _character.Components.Get<ICharacterMovable>().CanMove = false;

        public void DetectionReleased()
            => OnDetectionReleased?.Invoke(this);
    }
}