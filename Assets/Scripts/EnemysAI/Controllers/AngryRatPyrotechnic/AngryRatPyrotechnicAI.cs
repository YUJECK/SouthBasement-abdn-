using UnityEngine;

namespace EnemysAI
{
    public class AngryRatPyrotechnicAI : StateMachine
    {
        //private Animator animator;
        //private Shooting shooting;
        //private Health health;

        ////Типо геттеры и сеттеры
        //public override void ResetStun(bool stopChange, bool blockChange)
        //{
        //    if (blockChange) moving.SetBlocking(false);
        //    if (stopChange) moving.SetStop(false);
        //    isStopped = false;
        //    animator.SetBool("isStunned", false);
        //    shooting.SetStop(true);
        //}
        //public override void SetStun(bool stopChange, bool blockChange)
        //{
        //    if (blockChange) moving.SetBlocking(true);
        //    if (stopChange) moving.SetStop(true);
        //    isStopped = true;
        //    animator.SetBool("isStunned", true);
        //    shooting.SetStop(true);
        //}

        //private void Start()
        //{
        //    animator = GetComponent<Animator>();
        //    moving = GetComponent<Move>();
        //    health = GetComponent<EnemyHealth>();
        //    moving.speed = walkSpeed;

        //    //События
        //    targetSelection.onSetTarget.AddListener(CheckTarget);
        //    targetSelection.onResetTarget.AddListener(CheckTarget);
        //    GetComponent<Sleeping>().GoSleep();
        //}
        //private void Update()
        //{
        //    if (!isStopped)
        //    {
        //        if (animator != null && moving != null)//Анимация
        //        {
        //            //Анимация бега
        //            if (moving.isNowWalk && !animator.GetBool("isRun")) animator.SetBool("isRun", true);
        //            if ((!moving.isNowWalk || moving.GetStop()) && animator.GetBool("isRun")) animator.SetBool("isRun", false);
        //        }
        //        moving.DynamicPathfind();
        //        moving.Moving();
        //    }
        //}
    }
}