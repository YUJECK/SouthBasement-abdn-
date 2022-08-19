using UnityEngine;
using EnemysAI.CombatSkills;
using EnemysAI.Other;

namespace EnemysAI.Moving
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private CreaturePreset preset;
        [SerializeField] protected Move moving;
        [SerializeField] protected Flipping flipping;
        [SerializeField] protected DynamicPathFinding dynamicPathFinding;
        //Combat skills
        [SerializeField] protected Combat combat;
        [SerializeField] protected Health health;
        [SerializeField] protected Shooting shooting;
        //Other
        [SerializeField] protected TargetSelection targetSelection;
        [SerializeField] protected TriggerChecker stopTrigger;
        [SerializeField] protected Sleeping sleeping;

        public Move Moving => moving;
        public Flipping Flipping => flipping;
        public DynamicPathFinding DynamicPathFinding => dynamicPathFinding;
        public Combat Combat => combat;
        public Shooting Shooting => shooting;
        public Health Health => health;
        public TargetSelection TargetSelection => targetSelection;
        public Sleeping Sleeping => sleeping;
        public TriggerChecker StopTrigger => stopTrigger;

        void Start() => preset.Init(this);
    }
}
