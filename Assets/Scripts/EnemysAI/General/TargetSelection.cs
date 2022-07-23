using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI
{
    public class TargetSelection : MonoBehaviour
    {
        //Приватные поля
        [SerializeField] private List<string> blackTagList = new List<string>(); //Список тегов которые не будут читаться 
        private EnemyTarget _target; //Текущий таргет
        private List<EnemyTarget> _targets = new List<EnemyTarget>(); //Список всех таргетов
        
        //Публичные поля
        public EnemyTarget target
        {
            get => _target;
            private set => _target = value;
        }
        public List<EnemyTarget> targets
        {
            get => _targets;
            private set => _targets = value;
        }

        //Ивенты
        [Header("События")]
        public UnityEvent<EnemyTarget> onSetTarget = new UnityEvent<EnemyTarget>();
        public UnityEvent<EnemyTarget> onTargetChange = new UnityEvent<EnemyTarget>();
        public UnityEvent<EnemyTarget> onResetTarget = new UnityEvent<EnemyTarget>();

        //Методы взаимодействия
        public void SetNewTarget() 
        {
            EnemyTarget newTarget = FindNewTarget();

            //Вызов ивентов
            if (newTarget != null && _target != newTarget) { onTargetChange.Invoke(newTarget); }
            onSetTarget.Invoke(newTarget);
            _target = newTarget;
        }
        private EnemyTarget FindNewTarget()
        {
            bool isSamePriority = true;
            EnemyTarget target = null;
            int priority = _targets[0].priority;

            for (int i = 0; i < _targets.Count; i++)
            //Проверяем все таргеты по приоритету
            {
                if (_targets[i].priority == priority) continue;
                else //Если приоритет не одинаковый
                {
                    isSamePriority = false;
                    target = _targets[_targets.Count - 1];
                    break;
                }
            }
            if (isSamePriority)
            //Если у всех таргетов одинаковый приоритет
            {
                int rand = Random.Range(0, _targets.Count);
                target = _targets[rand];
            }
            //Выводим сообщение в консоль если ничего не нашли, не думаю что сюда код вообще попадет
            if (target == null) FindObjectOfType<RatConsole>().DisplayText("Таргет не был найден", Color.red,
                RatConsole.Mode.ConsoleMessege, "<TargetSelection.cs, line 42>");

            return target;
        }

        //Методы QuickSort-а
        private List<EnemyTarget> QuickSort(List<EnemyTarget> targets, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex) return targets;

            int pivot = GetPivotInd(targets, minIndex, maxIndex);

            QuickSort(targets, minIndex, pivot - 1);
            QuickSort(targets, pivot + 1, maxIndex);

            return new List<EnemyTarget>();
        }
        private int GetPivotInd(List<EnemyTarget> targets, int minIndex, int maxIndex)
        {
            int pivot = minIndex - 1;

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (targets[i].priority < targets[maxIndex].priority)
                {
                    pivot++;
                    Swap(ref targets, pivot, i);
                }
            }

            pivot++;
            Swap(ref targets, pivot, maxIndex);

            return pivot;
        }
        private void Swap(ref List<EnemyTarget> targets, int firstInd, int secondInd)
        {
            EnemyTarget tmp = targets[firstInd];
            targets[firstInd] = targets[secondInd];
            targets[secondInd] = tmp;
        }
        
        //Обработка объектов которые зашли/вышли в/из триггера
        public void OnTriggerEnter2D(Collider2D coll) //Вход в поле зрения
        {
            if (!blackTagList.Contains(coll.tag))
            {
                if (coll.TryGetComponent(typeof(EnemyTarget), out Component comp))
                {
                    EnemyTarget newTarget = coll.GetComponent<EnemyTarget>();
                    if(!_targets.Contains(newTarget))
                    {
                        _targets.Add(newTarget);
                        QuickSort(_targets, 0, _targets.Count - 1);
                        SetNewTarget();
                    }
                }
            }
        }
        public void OnTriggerExit2D(Collider2D coll) //Выход из поля зрения
        {
            if (coll.TryGetComponent(typeof(EnemyTarget), out Component comp))
            {
                EnemyTarget exitTarget = coll.GetComponent<EnemyTarget>();
                if (_targets.Contains(exitTarget))
                {
                    _targets.Remove(exitTarget);
                    
                    //Если таргет который вышел за пределы поля зрения действующий таргет, то мы заново ищем таргет
                    if (_target == exitTarget)
                    {
                        _target = null;
                        if (_targets.Count != 0) _target = FindNewTarget();
                    }

                    onResetTarget.Invoke(exitTarget);
                }
            }
        }
    }
}