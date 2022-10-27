using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingPattern : ScriptableObject
{
    public void UsePattern(Shooting shooting) => shooting.StartCoroutine(Pattern(shooting));
    public abstract IEnumerator Pattern(Shooting shooting);
}