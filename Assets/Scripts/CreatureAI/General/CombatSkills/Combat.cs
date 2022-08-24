using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.CombatSkills
{
    [RequireComponent(typeof(PointRotation))]
    [AddComponentMenu("Creature/General/CombatSkills/Combat")]
    public class Combat : MonoBehaviour
    {
        [SerializeField] private Transform attackPoint; // Точка атаки
        private PointRotation pointRotation;

        [Header("Параметры атаки")]
        [SerializeField] private int minDamage = 10;
        [SerializeField] private int maxDamage = 10;
        [SerializeField] private float attackRange = 0.5f; // Радиус атаки
        [SerializeField] private float attackRate = 3f; // Периодичность атаки
        [SerializeField] private float attackTimeOffset = 0.6f; // Время когда сработает корутина

        [Header("События")]
        public UnityEvent onAttack = new UnityEvent(); // При атаке
        public UnityEvent beforeAttack = new UnityEvent(); // За attackTimeOffset до атаки
        public UnityEvent afterAttack = new UnityEvent();
        public UnityEvent onEnterArea = new UnityEvent(); // Когда зашел в радиус активации атаки

        [Header("Определение цели")]
        [SerializeField] private LayerMask damageLayer; // Дамажный слой
        [SerializeField] private List<string> enterTags = new List<string>(); // Тег на проверку у тригера

        private bool isOnTrigger = false;
        private bool isStopped = false;
        private Transform attackTarget;

        //Методы управления скриптом
        public void Attack() => StartCoroutine(StartAttack(attackTimeOffset));

        //Методы атаки
        private IEnumerator StartAttack(float waitTime)
        {
            while(!isStopped && isOnTrigger)
            {
                beforeAttack.Invoke();
                yield return new WaitForSeconds(attackTimeOffset);
                Hit();
                afterAttack.Invoke();
                yield return new WaitForSeconds(attackRate);
            }
        }
        private bool Hit()
        {
            //Определяем все объекты попавшие в радиус атаки
            bool hasHitted = false;
            Collider2D[] hitObj = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, damageLayer);
            pointRotation.StopRotating(true, 0.8f);
            onAttack.Invoke();

            //Проверяем каждый их них на наличие комнонента Health
            foreach (Collider2D obj in hitObj)
            {
                hasHitted = true;
                if (obj.TryGetComponent(typeof(PlayerHealth), out Component comp))
                    obj.GetComponent<PlayerHealth>().TakeHit(Random.Range(minDamage, maxDamage + 1));
            }
            
            return hasHitted;
        }

        //Типо сеттеры и геттеры
        public void SetStop(bool active) { isStopped = active; }
        public bool GetStop() { return isStopped; }
        public bool IsOnTrigger => isOnTrigger;

        //Юнитивские методы
        private void Awake() 
        {
            pointRotation = GetComponent<PointRotation>();
            if(enterTags.Count == 0) enterTags.Add("Player");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (enterTags.Contains(collision.tag) && attackTarget != collision.transform)
            {
                isOnTrigger = true;
                attackTarget = collision.transform;
                onEnterArea.Invoke();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (attackTarget == collision.transform)
            {
                isOnTrigger = false;
                attackTarget = null;
            }
        }
        void OnDrawGizmosSelected()//Отрисовка радиуса атаки
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}