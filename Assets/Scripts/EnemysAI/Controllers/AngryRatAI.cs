using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace EnemysAI
{
    [RequireComponent(typeof(Move))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    public class AngryRatAI : EnemyAI
    {
        //Ссылки на другие классы
        private Animator animator; 
        [SerializeField] private Combat combat; 
        private EnemyHealth health;

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

        //Типо сеттеры и геттеры
        public override void GoSleep()
        {
            if (!isSleep)
            {
                isSleep = true;
                onSleep.Invoke();
                if (animator.GetBool("isRun")) animator.SetBool("isRun", false);
                moving.enabled = false;
                health.enabled = false;
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(false);
                targetSelection.gameObject.SetActive(true);
            }
        }
        public override void WakeUp()
        {
            if (isSleep)
            {
                isSleep = false;
                onWakeUp.Invoke();
                moving.enabled = true;
                health.enabled = true;

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        //Юнитивские методы
        private void Start()
        {
            animator = GetComponent<Animator>();
            moving = GetComponent<Move>();
            health = GetComponent<EnemyHealth>();
            moving.speed = walkSpeed;

            //События
            GetComponent<EnemyHealth>().stun.AddListener(GetStunned);
            targetSelection.onTargetChange.AddListener(CheckTarget);
            targetSelection.onResetTarget.AddListener(CheckTarget);
            GoSleep();
        }
        private void Update() //Анимации
        {
            if (!isSleep && !isStopped)
            {
                if (animator != null && moving != null)//Анимация
                {
                    //Анимация бега
                    if (moving.isNowWalk && !animator.GetBool("isRun")) animator.SetBool("isRun", true);
                    if ((!moving.isNowWalk || moving.GetStop()) && animator.GetBool("isRun")) animator.SetBool("isRun", false);
                }
            }
        }
    }
}