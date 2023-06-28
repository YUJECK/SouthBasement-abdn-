using System.Collections;
using UnityEngine;

namespace SouthBasement.Infrastucture
{
    public sealed class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        private void Awake()
        {
        }

        public Coroutine Run(IEnumerator coroutine)
        {
            gameObject.SetActive(true);
            GetComponent<CoroutineRunner>().enabled = true;
            
            return StartCoroutine(coroutine);
        }

        public void Stop(IEnumerator coroutine) => StopCoroutine(coroutine);
        public void Stop(Coroutine coroutine) => StopCoroutine(coroutine);
    }
}