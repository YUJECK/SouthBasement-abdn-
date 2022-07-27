using UnityEngine;

namespace EnemysAI
{
    public class AngryRatPyrotechnicAI : EnemyAI
    {
        private Animator animator;
        private Shooting shooting;
        private Health health;

        //Типо геттеры и сеттеры
        public override void GoSleep()
        {
            if (!isSleep)
            {
                isSleep = true;
                onSleep.Invoke();
                if (animator.GetBool("isRun")) animator.SetBool("isRun", false);
                moving.enabled = false;
                health.effectHandler.enabled = false;
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
                moving.enabled = true;
                health.enabled = true;
                health.effectHandler.enabled = true;

                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(true);
                onWakeUp.Invoke();
            }
        }
        public override void ResetStun(bool stopChange, bool blockChange)
        {
            if (blockChange) moving.SetBlocking(false);
            if (stopChange) moving.SetStop(false);
            isStopped = false;
            animator.SetBool("isStunned", false);
            shooting.SetStop(true);
        }
        public override void SetStun(bool stopChange, bool blockChange)
        {
            if (blockChange) moving.SetBlocking(true);
            if (stopChange) moving.SetStop(true);
            isStopped = true;
            animator.SetBool("isStunned", true);
            shooting.SetStop(true);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            moving = GetComponent<Move>();
            health = GetComponent<EnemyHealth>();
            moving.speed = walkSpeed;

            //События
            targetSelection.onSetTarget.AddListener(CheckTarget);
            targetSelection.onResetTarget.AddListener(CheckTarget);
            GoSleep();
        }
        private void Update()
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