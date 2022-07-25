using UnityEngine;
using EnemysAI;
using UnityEngine.Events;

public abstract class ShootingPattern : ScriptableObject
{
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onEnter = new UnityEvent();
    protected bool _isWork = false;
    public bool isWork
    {
        get { return _isWork; }
        private set { _isWork = value; }
    }
    public abstract void StartPattern(Shooting shooting);
    public abstract void StopPattern(Shooting shooting);
}