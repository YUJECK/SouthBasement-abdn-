using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI.Moving
{
    public class Moving : MonoBehaviour
    {
        [Header("Настройки движения")]
        [SerializeField] private float speed = 3; // Скорость передвижения 
        public UnityEvent onArrive = new UnityEvent(); //Когда придет к конечной точке пути
        
        private List<Vector2> path = new List<Vector2>(); //Путь по которому идет объект
        private bool isNowWalk = false; //Идет ли сейчас объект
        private bool isStopped = false;
        private bool blockStop = false;

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
        public void SetStop(bool stopped) => isStopped = stopped;
        public bool IsStopped => isStopped;

        private void FixedUpdate() //Физическая логика
        {
            //Движение 
            if (path.Count != 0)
            {
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
    }
}