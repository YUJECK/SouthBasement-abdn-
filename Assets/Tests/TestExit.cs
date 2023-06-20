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

        public void Interact()
        {
            SceneManager.LoadScene(3);
        }

        public void DetectionReleased()
        {
            
        }
    }
}