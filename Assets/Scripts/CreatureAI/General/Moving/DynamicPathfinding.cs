using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Moving
{
    [AddComponentMenu("Creature/General/Moving/Dynamic Pathfinfing")]
    [RequireComponent(typeof(Pathfinder))]
    public class DynamicPathfinding : MonoBehaviour
    {
        [Header("Настройки динамического поиска пути")]
        [SerializeField] private float searchRate = 1f; //Частота поиска пути
        public UnityEvent<List<Vector2>> whenANewPathIsFound = new UnityEvent<List<Vector2>>();

        private Utility.ComponentWorkState workState; //Состояние работы
        private Transform target; //Таргет для поиска пути
        private List<Vector2> path = new List<Vector2>(); //Сам путь
        private Pathfinder pathfinder; 

        //Методы взаимодействия и геттеры, сеттеры
        public void StartDynamicPathfinding() { StartCoroutine(DynamicPathfindingCoroutine()); workState = Utility.ComponentWorkState.Working; }
        public void StopDynamicPathfinding() { StopCoroutine(DynamicPathfindingCoroutine()); workState = Utility.ComponentWorkState.Stopped; }
        public float SearchRate
        {
            get => searchRate;
            set
            {
                if (value < 0.5) value = 0.5f;
                if (value > 6) value = 6;

                searchRate = value;
            }
        }
        public Utility.ComponentWorkState WorkState => workState;
        public Transform Target => target;
        public void SetNewTarget(EnemyTarget target) => this.target = target.transform;
        public void ResetTarget() => target = null;

        //Приватные методы
        private IEnumerator DynamicPathfindingCoroutine()
        {
            while (true)
            {
                if (workState == Utility.ComponentWorkState.Working && target != null)
                {
                    path = FindPath();
                    whenANewPathIsFound.Invoke(path);
                }
                yield return new WaitForSeconds(searchRate);
            }
        }
        private List<Vector2> FindPath()
        {
            return pathfinder.FindPath(
                new Vector2(transform.position.x / ManagerList.Grid.NodeSize, transform.position.y / ManagerList.Grid.NodeSize),
                new Vector2(target.transform.position.x / ManagerList.Grid.NodeSize, target.transform.position.y / ManagerList.Grid.NodeSize), false);
        }

        //Юнитивские методы
        private void Awake() => pathfinder = GetComponent<Pathfinder>();
    }
}