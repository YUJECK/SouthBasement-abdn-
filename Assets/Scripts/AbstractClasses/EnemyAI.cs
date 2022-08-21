using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI
{
    public enum EnemyState
    {
        Idle,
        Sleeping,
        WakeUp,
        Walking,
        Attacking,
        Heeling,
        Stunned
    }
    public class State
    {
        private EnemyState state = EnemyState.Idle;
        private Animator animator;
        private string animation = "No animation";
        private UnityEvent[] actionsOnStart;
        private UnityEvent[] dynamicActions;
        private UnityEvent[] actionsOnEnd;

        public EnemyState EnemyState => state;
        public string Animation => animation;
        public Animator Animator => animator;
        public void InvokeStartActions(float delay) => ManagerList.GameManager.StartCoroutine(InvokeActions(delay, actionsOnStart));
        public void InvokeEndActions(float delay) => ManagerList.GameManager.StartCoroutine(InvokeActions(delay, actionsOnEnd));
        public void InvokeDynamicActions()
        {
            foreach (UnityEvent action in dynamicActions)
                action.Invoke();
        }
        private IEnumerator InvokeActions(float delay, UnityEvent[] actions)
        {
            yield return new WaitForSeconds(delay);
            foreach (UnityEvent aciton in actions)
                aciton.Invoke();
        }
    }

    public abstract class EnemyAI : MonoBehaviour
    {
        private List<State> states = new List<State>();
        private State currentState;
        private List<Vector2> path;

        public void ChangeState(State newState, float currentActionStopDelay, float newActionStartDelay)
        {
            if (currentState != null) currentState.InvokeEndActions(currentActionStopDelay);
            currentState = newState;
            currentState.InvokeStartActions(newActionStartDelay);
            if (currentState.Animation != "No animation") currentState.Animator.Play(currentState.Animation);
        }
    }
}
