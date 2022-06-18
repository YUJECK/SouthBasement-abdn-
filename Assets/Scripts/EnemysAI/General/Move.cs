using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class Move : MonoBehaviour
{
    public float speed
    {
        get { return speed; }
        set
        {
            if (value <= 0f)
            {
                speed = 1;
                return;
            }
            if (value > 8f)
            {
                speed = 8;
                return;
            }
            speed = value;
        }
    }
    public bool isStopped;
    public List<string> stopTags = new List<string>();
    private List<Vector2> path = new List<Vector2>();

    //Переменные для поворота
    private bool flippedOnRight;
    private Vector2 lastPos;

    //Ссылки на другие скрипты
    private TargetSelection targetSelection;
    private Pathfinding pathfinding;
    private Grid grid;
    private Rigidbody2D rb;

    private void FindPath(EnemyTarget target)
    {
        path = pathfinding.FindPath();
    }
    private void Flip() { transform.Rotate(0f, 180f, 0f); }

    private void FixedUpdate() //Физическая логика
    {
        //Сброс velocity
        if (rb != null) rb.velocity = Vector2.zero;

        //Поворот
        if (new Vector3(lastPos.x, lastPos.y, transform.position.z) != transform.position && !freezeFlip)
        {
            if (lastPos.x < transform.position.x && flippedOnRight)
            {
                Flip();
                flippedOnRight = false;
            }
            else if (lastPos.x > transform.position.x && !flippedOnRight)
            {
                Flip();
                flippedOnRight = true;
            }
        }

        lastPos = transform.position;

        //Движение 
        if (path.Count != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[0], speed * Time.deltaTime);
            isStopped = false;

            if (transform.position == new Vector3(path[0].x, path[0].y, transform.position.z))
            {
                path.RemoveAt(0);
                //Убираем коллайдер пути
                if (pathfinding.gridChanges.Count != 0)
                {
                    grid.grid[pathfinding.gridChanges[0].x, pathfinding.gridChanges[0].y] = 0;
                    pathfinding.gridChanges.RemoveAt(0);
                }
            }
        }
        else if (path.Count == 0)
        {
            //if (targetMoveType != TargetType.Movable) ResetTarget();
            isStopped = true;
        }
    }
}
