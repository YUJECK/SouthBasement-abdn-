using System;
using SouthBasement.Interactions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheRat.Tests
{
    public class TestExit : MonoBehaviour, IInteractive
    {
        public void Detect()
        {
            
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Interact()
        {
            SceneManager.LoadScene(3);
        }

        public void DetectionReleased()
        {
            
        }
    }
}