using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace EnemysAI
{
    [RequireComponent(typeof(Move))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Sleeping))]
    public class AngryRatAI : EnemyAI
    {
        //Ссылки на другие классы
        private Animator animator; 
        [SerializeField] private Combat combat; 
        private EnemyHealth health;
        private EffectHandler effectHandler;

        //Оглушение
        public override void SetStun(bool stopChange, bool blockChange)
        {
            if (stopChange) moving.SetStop(true);
            if (blockChange) moving.SetBlocking(true);
            combat.SetStop(true);
            isStopped = true;
            if (animator.GetBool("isRun")) animator.SetBool("isRun", false);
            animator.SetBool("isStunned", true);
        }
        public override void ResetStun(bool stopChange, bool blockChange)
        {
            if (blockChange) moving.SetBlocking(false);
            if (stopChange) moving.SetStop(false);
            combat.SetStop(false);
            isStopped = false;
            animator.SetBool("isStunned", false);
        }

        //Юнитивские методы
        private void Start()
        {
            animator = GetComponent<Animator>();
            moving = GetComponent<Move>();
            health = GetComponent<EnemyHealth>();
            moving.speed = walkSpeed;

            //События
            targetSelection.onSetTarget.AddListener(CheckTarget);
            targetSelection.onResetTarget.AddListener(CheckTarget);
            GetComponent<Sleeping>().GoSleep();
        }
        private void Update() //Анимации
        {
            if (!isStopped)
            {
                if (animator != null && moving != null)//Анимация
                {
                    //Анимация бега
                    if (moving.isNowWalk && !animator.GetCurrentAnimatorStateInfo(0).IsName("AngryRatRunning")) animator.Play("AngryRatRunning");
                    if ((!moving.isNowWalk || moving.GetStop()) && animator.GetCurrentAnimatorStateInfo(0).IsName("AngryRatRunning")) animator.Play("AngryRatIdle");
                }
            }
        }
    }
}