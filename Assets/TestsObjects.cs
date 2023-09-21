using UnityEngine;

namespace SouthBasement
{
    public sealed class TestsObjects : MonoBehaviour
    {
#if DEBUG
        private void Start()
        {
            gameObject.SetActive(true);    
        }
#endif

#if !DEBUG
        private void Awake()
        {
            gameObject.SetActive(false);    
        }
#endif
    }
}