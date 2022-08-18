using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI.Moving
{
    public class Flipping : MonoBehaviour
    {
        private enum DefinitionOfRotation
        {
            ByMoving,
            ByTarget
        }

        [Header("Настройки поворота")]
        [SerializeField] private DefinitionOfRotation definitionOfRotation;
        public UnityEvent onFlip = new UnityEvent();

        //Переменные для поворота
        private bool isStopped = false;
        private bool flippedOnRight = false;
        private Vector2 lastPos;
        private Transform targetToTurn;

        public bool IsStopped => isStopped;
        public void SetStop(bool stopped) => isStopped = stopped;
        //Логика
        private void CheckRotationByMoving()
        {
            //Поворот
            if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position)
            {
                if (lastPos.x > transform.position.x && flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = false;
                }
                else if (lastPos.x < transform.position.x && !flippedOnRight)
                {
                    FlipThisObject();
                    flippedOnRight = true;
                }
            }
            lastPos = transform.position;
        }
        private void CheckRotationByTarget()
        {
            if (targetToTurn != null)
            {
                if (transform.position.x != targetToTurn.position.x)
                {
                    if (transform.position.x > targetToTurn.position.x && flippedOnRight)
                    {
                        FlipThisObject();
                        flippedOnRight = false;
                    }
                    else if (transform.position.x < targetToTurn.position.x && !flippedOnRight)
                    {
                        FlipThisObject();
                        flippedOnRight = true;
                    }
                }
            }
        }

        //Метод поворота    
        private void FlipThisObject() { transform.Rotate(0f, 180f, 0f); onFlip.Invoke(); }
        public void FlipOther(Transform _transform) { _transform.Rotate(180f, 0f, 0f); }

        private void Update()
        {
            if(!IsStopped)
            {
                if (definitionOfRotation == DefinitionOfRotation.ByMoving) CheckRotationByMoving();
                if (definitionOfRotation == DefinitionOfRotation.ByTarget) CheckRotationByTarget();
            }
        }
    }
}