using System;
using TheRat.AI;
using UnityEngine;

namespace TheRat.Helpers.Rotator
{
    public sealed class EnemyTargetSetter : MonoBehaviour
    {
        [SerializeField] private TargetSelector targetSelector;

        private void Awake()
        {
            targetSelector.OnTargetFound += (target => GetComponent<ObjectRotator>().Target = target.transform);
        }
    }
}