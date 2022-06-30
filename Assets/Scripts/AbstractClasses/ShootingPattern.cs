using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingPattern : MonoBehaviour
{
    public abstract void StartPattern();
    public abstract void StopPattern();
}