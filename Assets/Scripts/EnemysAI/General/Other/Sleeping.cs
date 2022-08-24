using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI.Other
{
    [AddComponentMenu("EnemysAI/General/Other/Sleeping")]
    public class Sleeping : MonoBehaviour
    {
        private bool isSleep = false;
        [SerializeField] private float wakingUpTime = 0f;
        public UnityEvent onSleep = new UnityEvent();
        public UnityEvent beforeWakeUp = new UnityEvent();
        public UnityEvent onWakeUp = new UnityEvent();
        public List<Behaviour> componentsToDisable = new List<Behaviour>();

        public void GoSleep()
        {
            if (!isSleep)
            {
                isSleep = true;
                onSleep.Invoke();
                for (int i = 0; i < transform.childCount; i++) 
                    transform.GetChild(i).gameObject.SetActive(false);
                foreach (Behaviour behaviour in componentsToDisable)
                    behaviour.enabled = false;
            }
        }
        public void WakeUp() { if (isSleep) { beforeWakeUp.Invoke(); StartCoroutine(WakingUp()); } }
        public IEnumerator WakingUp()
        {
            yield return new WaitForSeconds(wakingUpTime);
            if (isSleep)
            {
                isSleep = false;
                onWakeUp.Invoke();

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);
                foreach (Behaviour behaviour in componentsToDisable)
                    behaviour.enabled = true;
            }
        }
    }
}
