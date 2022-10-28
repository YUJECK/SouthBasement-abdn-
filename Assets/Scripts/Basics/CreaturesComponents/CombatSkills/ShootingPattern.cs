using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class ShootingPattern : ScriptableObject
{
    public UnityEvent onPatternEnd = new UnityEvent();
    protected bool isFinished = false;

    public bool IsFinished => isFinished;

    public void UsePattern(Shooting shooting) => shooting.StartCoroutine(Pattern(shooting));
    public abstract IEnumerator Pattern(Shooting shooting);
}