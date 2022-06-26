using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealth))]
public class AngryRatAI : MonoBehaviour
{
    [Header("Параметры скорости")]
    [SerializeField] private float walkSpeed = 2f; //Скорость при ходьбе
    [SerializeField] private float runSpeed = 3.3f; //Скорость при беге
    private bool isStopped = false;
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

    //Оглушение
    public IEnumerator Stun(float duration)
    {
        SetStun();
        yield return new WaitForSeconds(duration);
        ResetStun();
    }
    public void GetStunned(float duration) { StartCoroutine(Stun(duration)); }
    public void SetStun()
    {
        moving.SetStop(true);
        moving.SetBlocking(true);
        combat.SetStop(true);
        isStopped = true;
        if (anim.GetBool("isRun")) anim.SetBool("isRun", false);
        anim.SetBool("isStunned", true);
    }
    public void ResetStun()
    {
        moving.SetBlocking(false);
        moving.SetStop(false);
        combat.SetStop(false);
        isStopped = false;
        anim.SetBool("isStunned", false);
    }

    //Типо сеттеры и геттеры
    public void SetStop(bool active) { isStopped = active; }
    public bool GetStop() { return isStopped; }

    //Юнитивские методы
    private void Start()
    {
        anim = GetComponent<Animator>();
        moving = GetComponent<Move>();
        moving.speed = walkSpeed;

        //События
        targetSelection.onTargetChange.AddListener(CheckTargetMoveType);
    }
    private void Update() //Основная логика
    {
        if (!isStopped)
        {
            if (anim != null && moving != null)//Анимация и атака
            {
                //Анимация бега
                if (moving.isNowWalk && !anim.GetBool("isRun")) anim.SetBool("isRun", true);
                if ((!moving.isNowWalk || moving.GetStop()) && anim.GetBool("isRun")) anim.SetBool("isRun", false);
            }
        }
    }
}