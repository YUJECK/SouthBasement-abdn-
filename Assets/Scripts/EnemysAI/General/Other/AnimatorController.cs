using UnityEngine;

namespace EnemysAI.Other
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private Animator animator;

        private void Awake() => animator = GetComponent<Animator>();
        public void Init(RuntimeAnimatorController controllerParameter) => animator.runtimeAnimatorController = controllerParameter;
        public void PlayAnimation(string animation) => animator.Play(animation);
    }
}