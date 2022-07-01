using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPyrotechnicPattern : ShootingPattern
{
    [SerializeField] private GameObject petard;
    public override void StartPattern()
    {
        onEnter.Invoke();
    }

    public override void StopPattern()
    {
        onExit.Invoke();
    }
}
