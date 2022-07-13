using UnityEngine;
using System.Collections.Generic;

public enum TargetType
{
    Static,
    Movable 
}
public class EnemyTarget : MonoBehaviour
{
    public int priority;
    public TargetType targetMoveType;
}
