using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Pathfinder))]
public class DynamicPathFinding : MonoBehaviour
{
    [Header("��������� ������������� ������ ����")]
    [SerializeField] private float searchRate = 1f; //������� ������ ����
    public UnityEvent<List<Vector2>> whenANewPathIsFound = new UnityEvent<List<Vector2>>();
    
    private bool isStopped = false; //���������� ��
    private Transform target;
    private List<Vector2> path = new List<Vector2>();
    private Pathfinder pathfinder;

    public void StartDynamicPathfinding() => StartCoroutine(DynamicPathfinding());
    public void StopDynamicPathfinding() => StartCoroutine(DynamicPathfinding());
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
    public void SetNewTarget(EnemyTarget target) => this.target = target.transform;
    public void ResetTarget() => target = null;

    private IEnumerator DynamicPathfinding()
    {
        while (true)
        {
            if (!isStopped && target != null)
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
    
    private void Awake()
    {
        pathfinder = GetComponent<Pathfinder>();
    }
}