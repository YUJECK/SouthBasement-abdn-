﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.Scripts.Helpers
{
    public sealed class TriggerCallback : MonoBehaviour
    {
        [SerializeField] private List<string> tagList = new();

        public event Action<Collider2D> OnTriggerEnter;
        public event Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(tagList.Contains(other.tag) || tagList.Count == 0)
                OnTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(tagList.Contains(other.tag) || tagList.Count == 0)
                OnTriggerExit?.Invoke(other);
        }
    }
}