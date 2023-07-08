using UnityEngine;

namespace TheRat
{
    public class BlackoutTransition : MonoBehaviour
    {
        private void Awake()
            => DontDestroyOnLoad(gameObject);
        public void PlayBlackout() 
            => GetComponent<Animator>().Play("ToBlack");
    }
}