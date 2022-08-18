using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace EnemysAI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Sleeping))]
    public class AngryRatAI : EnemyAI
    {
        ////Ссылки на другие классы
        //private Animator animator;
        //[Header("Анимации")]
        //[SerializeField] private string stunAnimation;
        //[SerializeField] private string runAnimation;
        //[SerializeField] private string idleAnimation;
        //[SerializeField] private string sleepingAnimation;
        //[SerializeField] private string wakeUpAnimation;
        //[Header("Другое")]
        //[SerializeField] private Combat combat; 

        ////Оглушение
        //public override void SetStun(bool stopChange, bool blockChange)
        //{
        //    if (stopChange) moving.SetStop(true);
        //    if (blockChange) moving.SetBlocking(true);
        //    combat.SetStop(true);
        //    isStopped = true;
        //    animator.Play(stunAnimation);
        //}
        //public override void ResetStun(bool stopChange, bool blockChange)
        //{
        //    if (blockChange) moving.SetBlocking(false);
        //    if (stopChange) moving.SetStop(false);
        //    combat.SetStop(false);
        //    isStopped = false;
        //    animator.Play(idleAnimation);
        //}

        ////Юнитивские методы
        //private void Start()
        //{
        //    animator = GetComponent<Animator>();
        //    moving = GetComponent<Move>();
        //    moving.speed = walkSpeed;

        //    //События
        //    targetSelection.onSetTarget.AddListener(CheckTarget);
        //    targetSelection.onResetTarget.AddListener(CheckTarget);
        //    GetComponent<Sleeping>().GoSleep();
        //}
        //private void Update() //Анимации
        //{
        //    if (!isStopped)
        //    {
        //        if (animator != null && moving != null)//Анимация
        //        {
        //            //Анимация бега
        //            if (moving.isNowWalk && !animator.GetCurrentAnimatorStateInfo(0).IsName(runAnimation)) animator.Play(runAnimation);
        //            if ((!moving.isNowWalk || moving.GetStop()) && animator.GetCurrentAnimatorStateInfo(0).IsName(runAnimation)) animator.Play(runAnimation);
        //        }
        //    }
        //}
    }
}