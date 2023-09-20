using SouthBasement.Helpers.Rotator;
using UnityEngine;

namespace SouthBasement
{
    public sealed class CarrotDroneRotator : ObjectRotator
    {
        protected override void FixedUpdate()
        {
            if (Target == null)
            {
                transform.Rotate(Vector3.forward, 1.2f);
            }
            else
            {
                base.FixedUpdate();
            }
        }
    }
}