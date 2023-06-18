using UnityEngine;

namespace SouthBasement.Helpers.Rotator
{
    [RequireComponent(typeof(ObjectRotator))]
    public sealed class MouseTargetSetter : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<ObjectRotator>().Target = GameObject.FindWithTag("Mouse").transform;
        }
    }
}