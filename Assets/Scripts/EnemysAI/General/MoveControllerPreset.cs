using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveControllerPreset : ScriptableObject
{
    public abstract void Init(MoveController moveController);
}
