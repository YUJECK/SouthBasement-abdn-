using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ShootingPattern : ScriptableObject
{
    public UnityEvent onExit = new UnityEvent();
    public UnityEvent onEnter = new UnityEvent();
    public abstract void StartPattern();
    public abstract void StopPattern();
}