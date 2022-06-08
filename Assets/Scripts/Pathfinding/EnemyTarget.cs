using UnityEngine;

public enum TargetType
{
    Static,
    Movable 
}
public class EnemyTarget : MonoBehaviour
{
    public int priority;
    public bool TargetType;
}
