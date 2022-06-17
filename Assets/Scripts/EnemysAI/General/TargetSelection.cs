using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetSelection : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TargetType targetMoveType = TargetType.Static; //Подвижный ли таргет
    public TriggerCheker areaCheker; //Область в которой враг будет идти за игроком
    [SerializeField] private List<EnemyTarget> targets; //Точки для передвижения

    //Ивенты
    public UnityEvent SetTarget = new UnityEvent();
    public UnityEvent ResetTarget = new UnityEvent();

    private EnemyTarget FindNewTarget()
    {
        bool isSamePriority = true;
        EnemyTarget target = null;
        int priority = targets[0].priority;

        for (int i = 0; i < targets.Count; i++)
        //Проверяем все таргеты по приоритету
        {
            if (targets[i].priority == priority) continue;
            else //Если приоритет не одинаковый
            {
                isSamePriority = false;
                target = targets[targets.Count - 1];
                break;
            }
        }
        if (isSamePriority)
        //Если у всех таргетов одинаковый приоритет
        {
            int rand = Random.Range(0, targets.Count);
            target = targets[rand];
        }
        //Выводим сообщение в консоль если ничего не нашли
        else FindObjectOfType<RatConsole>().DisplayText("Таргет не был найден", Color.red,
            RatConsole.Mode.ConsoleMessege, "<AngryRatAI.cs, line 132>");

        //Не думаю что сюда код вообще попадет
        return target;
    }
    public void OnAreaExit(GameObject obj) //Метод который будет вызываться при выходе за границу поля зрения врага
    {
        if (obj.TryGetComponent(typeof(EnemyTarget), out Component comp))
        {
            if (targets.Contains(obj.GetComponent<EnemyTarget>()))
            {
                //Если таргет который вышел за пределы поля зрения действующий таргет, то мы ресетаем
                if (target == obj.GetComponent<EnemyTarget>()) ResetTarget.Invoke();
                
                targets.Remove(obj.GetComponent<EnemyTarget>());
            }
        }
    }
    public void OnAreaEnter(GameObject obj) //Метод который будет вызываться при входе в поле зрения
    {
        if (obj.TryGetComponent(typeof(EnemyTarget), out Component comp))
        {
            EnemyTarget newTarget = obj.GetComponent<EnemyTarget>();
            if (!targets.Contains(newTarget))
            {
                targets.Add(newTarget);
                QuickSort(targets, 0, targets.Count - 1);
                target = FindNewTarget().transform;
            }
        }
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
}
