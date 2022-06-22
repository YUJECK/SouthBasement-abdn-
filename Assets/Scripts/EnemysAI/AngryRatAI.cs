using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(HealthEnemy))]
public class AngryRatAI : MonoBehaviour
{
    [Header("Параметры скорости")]
    [SerializeField] private float walkSpeed = 2f; //Скорость при ходьбе
    [SerializeField] private float runSpeed = 3.3f; //Скорость при беге
    [HideInInspector] public Effect stun;
    private EnemyTarget target; //Подвижный ли таргет

    //Ссылки на другие классы
    [Header("Другие скрипты")]
    [SerializeField] private Combat combat;
    [SerializeField] private TargetSelection targetSelection;
    private Animator anim; //Ссылка на аниматор объекта
    private Move moving;

    private IEnumerator ChangeSpeed(TargetType moveType) //Плавный переход скорости
    {
        float nextSpeed;
        if (moveType == TargetType.Static) nextSpeed = walkSpeed;
        else nextSpeed = runSpeed;

        float k = (nextSpeed - moving.speed) / 20;
        int n = (int)((nextSpeed - moving.speed) / k);
        if (n < 0f) n *= -1;

        for (int i = 0; i < n; i++)
        {
            yield return new WaitForSeconds(0.25f);
            moving.speed += k;
        }
    }
    private void CheckTargetMoveType(EnemyTarget target) //Смена скорости
    {
        if (this.target == target) return;
        else if (this.target == null || this.target.targetMoveType != target.targetMoveType) StartCoroutine(ChangeSpeed(target.targetMoveType));

        this.target = target;
    }
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
        moving.isStopped = false;
        combat.isStopped = false;
        stun.durationTime = 0f;
        anim.SetBool("isStunned", false);
    }

    //Юнитивские методы
    private void Start()
    {
        anim = GetComponent<Animator>();
        moving = GetComponent<Move>();
        moving.speed = walkSpeed;

        //События
        targetSelection.onTargetChange.AddListener(CheckTargetMoveType);
        GetComponent<HealthEnemy>().stun.AddListener(SetStun);
    }
    private void Update() //Основная логика
    {
        if (stun.durationTime == 0f)
        {
            if (anim != null && moving != null)//Анимация и атака
            {
                //Анимация бега
                if (moving.isNowWalk && !anim.GetBool("isRun")) anim.SetBool("isRun", true);
                if ((!moving.isNowWalk || moving.isStopped) && anim.GetBool("isRun")) anim.SetBool("isRun", false);
            }
        }
        //Сброс оглушения
        if (stun.durationTime != 0f && Time.time - stun.startTime >= stun.durationTime) ResetStun();
    }
}