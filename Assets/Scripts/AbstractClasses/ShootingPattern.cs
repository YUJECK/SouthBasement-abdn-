using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ShootingPattern : ScriptableObject
{
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onEnter = new UnityEvent();
    public bool isWork = false;
    public abstract void StartPattern(Shooting shooting, float startTimeOffset);
    public abstract void StopPattern(Shooting shooting);
}