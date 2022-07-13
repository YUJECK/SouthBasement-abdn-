using UnityEngine;
using EnemysAI;
using UnityEngine.Events;

public abstract class ShootingPattern : ScriptableObject
{
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onEnter = new UnityEvent();
    public bool isWork = false;
    public abstract void StartPattern(Shooting shooting);
    public abstract void StopPattern(Shooting shooting);
}