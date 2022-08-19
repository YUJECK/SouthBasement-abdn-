using EnemysAI;
using EnemysAI.Moving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreaturePreset : ScriptableObject
{
    //Moving
    public bool useMoving;
    public bool useFlipping;
    public bool useDynamicPathFinding;
    //Combat skills
    public bool useCombat;
    public bool useHealth;
    public bool useShooting;
    //Other
    public bool useTargetSelection;
    public bool useSleeping;
    
    public abstract void Init(Creature moveController);
}
