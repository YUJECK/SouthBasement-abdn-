using System.Collections.Generic;
using UnityEngine;

public class AngryRatAI : MonoBehaviour
{
    [Header("Параметры поведения")]
    [HideInInspector] public Effect stun;
    [SerializeField] private TargetType targetMoveType = TargetType.Static; //Подвижный ли таргет
    [SerializeField] private float walkSpeed = 2f; //Скорость при ходьбе
    [SerializeField] private float runSpeed = 3.3f; //Скорость при беге

    //Ссылки на другие классы
    private Animator anim; //Ссылка на аниматор объекта
    private Move moving;
    [SerializeField] private Combat combat;

    public void SetStun(float stunTime)
    { 
        stun.durationTime = stunTime; 
        stun.startTime = Time.time;
        moving.isStopped = true;
        combat.isStopped = true;
        if (anim.GetBool("isRun")) anim.SetBool("isRun", false);
        anim.SetBool("isStunned", true);
    }
    public void ResetStun()
    {
        moving.isStopped = true;
        combat.isStopped = true;
        stun.durationTime = 0f;
        anim.SetBool("isStunned", false);
    }

    //Юнитивские методы
    private void Start()
    {
        anim = GetComponent<Animator>();
        moving = GetComponent<Move>();
        Debug.Log(anim.name);
        //speed = walkSpeed;
        GetComponent<HealthEnemy>().stun.AddListener(SetStun); //!
    }
    private void Update() //Основная логика
    {
        if (stun.durationTime == 0f)
        {
            if (anim != null && moving != null)//Анимация и атака
            {
                //Анимация бега
                if (moving.isNowWalk && !anim.GetBool("isRun")) anim.SetBool("isRun", true);
                if (!moving.isNowWalk && anim.GetBool("isRun")) anim.SetBool("isRun", false);
            }
        }  
        //Сброс оглушения
        if (Time.time - stun.startTime >= stun.durationTime) ResetStun();
    }
}