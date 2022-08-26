using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Moving
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Creature/General/Moving/Move")]
    public class Move : MonoBehaviour
    {
        [Header("Настройки движения")]
        [SerializeField] private float speed = 3; // Скорость передвижения 
        public UnityEvent onBeginingOfMoving = new UnityEvent(); //Начало ходьбы
        public UnityEvent onArrive = new UnityEvent(); //Когда придет к конечной точке пути
        
        //Приватные переменные
        private List<Vector2> path = new List<Vector2>(); //Путь по которому идет объект
        private bool isNowWalk = false; //Идет ли сейчас объект
        private bool isStopped = false;
        private bool blockingStop = false;
        private List<string> stops = new List<string>();
        new private Rigidbody2D rigidbody;
        
        //Основной метод ходьбы
        public void Moving()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;

            if (path.Count != 0 && stops.Count == 0)
            {
                if (!isNowWalk) onBeginingOfMoving.Invoke();
                transform.position = Vector2.MoveTowards(transform.position, path[0], Speed * Time.deltaTime);
                isNowWalk = true;

                if (transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
                    path.RemoveAt(0);
            }
            else
            {
                isNowWalk = false;
                onArrive.Invoke();
            }
        }

        //Геттеры, сеттеры
        public float Speed
        {
            get => speed;
            set
            {
                if (value <= 0f) value = 1;
                if (value > 8f) value = 8;
                
                speed = value;
            }
        }
        public void SetPath(List<Vector2> path) => this.path = path;
        public void AddStop(string key)
        {
            if (stops.Contains(key)) Debug.LogWarning("Stops already contain key - " + key);
            else stops.Add(key);
        }
        public void RemoveAStop(string key) 
        {
            if (stops.Contains(key)) stops.Remove(key);
            else Debug.LogWarning("Stops don't contain key - " + key );
        }
        public void BlockStop(bool blocked) => blockingStop = blocked; 
        public bool IsStopped => isStopped;
        
        //Юнитивские методы
        private void Awake() => rigidbody = GetComponent<Rigidbody2D>();
    }
}