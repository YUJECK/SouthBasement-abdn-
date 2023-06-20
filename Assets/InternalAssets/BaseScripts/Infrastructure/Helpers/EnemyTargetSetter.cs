using System;
using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement.Helpers.Rotator
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