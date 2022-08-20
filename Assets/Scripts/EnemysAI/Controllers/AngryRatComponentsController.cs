using EnemysAI.Other;
using EnemysAI.CombatSkills;
using EnemysAI.Moving;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EnemysAI.Controllers
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(Sleeping))]
    public class AngryRatComponentsController : EnemyAI
    {
        private DynamicPathFinding dynamicPathFinding;
        [SerializeField] private TargetSelection targetSelection;
        private Move move;
        private void Start()
        {
            dynamicPathFinding = GetComponent<DynamicPathFinding>();
            move = GetComponent<Move>();
            dynamicPathFinding.whenANewPathIsFound.AddListener(move.SetPath);
            targetSelection.onSetTarget.AddListener(dynamicPathFinding.SetNewTarget);
        }
    }
}