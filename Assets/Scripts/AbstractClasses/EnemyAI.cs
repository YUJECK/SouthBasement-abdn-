using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI
{
    public abstract class EnemyAI : MonoBehaviour
    {
        [Header("Параметры скорости")]
        [SerializeField] protected float walkSpeed = 2f; //Скорость при ходьбе
        [SerializeField] protected float runSpeed = 3.3f; //Скорость при беге
        protected EnemyTarget target; //Подвижный ли таргет

        //Всякие приватные поля
        protected bool isStopped = false;

        //Ссылки на другие классы
        [Header("Другое")]
        [SerializeField] private bool wakeUpOnSettingTarget = false;
        [SerializeField] protected UnityEvent onSleep = new UnityEvent();
        [SerializeField] protected UnityEvent onWakeUp = new UnityEvent();

        [Header("Другие компоненты")]
        [SerializeField] protected TargetSelection targetSelection;
        protected Move moving;

        protected IEnumerator ChangeSpeed(TargetType moveType) //Плавный переход скорости
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
        protected void CheckTarget(EnemyTarget newTarget)
        {
            if (targetSelection.Targets.Count > 0)
            {
                if (target == newTarget) return;
                else if (target == null || target.targetMoveType != newTarget.targetMoveType) StartCoroutine(ChangeSpeed(newTarget.targetMoveType));
                target = newTarget;
            }
        }

        //Оглушение
        public IEnumerator Stun(float duration)
        {
            SetStun(true, true);
            yield return new WaitForSeconds(duration);
            ResetStun(true, true);
        }
        public void GetStunned(float duration) => StartCoroutine(Stun(duration));
        public abstract void SetStun(bool stopChange, bool blockChange);
        public abstract void ResetStun(bool stopChange, bool blockChange);

        //Типо сеттеры и геттеры
        public void SetStop(bool active) { isStopped = active; }
        public bool GetStop => isStopped; 
    }
}
