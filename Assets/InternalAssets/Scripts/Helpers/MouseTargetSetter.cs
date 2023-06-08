using System;
using UnityEngine;

namespace TheRat.InternalAssets.Scripts.Helpers
{
    [RequireComponent(typeof(Rotator))]
    public sealed class MouseTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Rotator>().Target = GameObject.FindWithTag("Mouse").transform;
        }
    }
}