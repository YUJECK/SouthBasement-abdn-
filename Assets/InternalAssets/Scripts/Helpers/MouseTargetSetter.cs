using System;
using UnityEngine;

namespace TheRat.InternalAssets.Scripts.Helpers
{
    [RequireComponent(typeof(AttackRotator))]
    public sealed class MouseTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<AttackRotator>().Target = GameObject.FindWithTag("Mouse").transform;
        }
    }
}