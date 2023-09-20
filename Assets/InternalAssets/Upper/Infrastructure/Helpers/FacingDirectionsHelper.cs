using SouthBasement.Enums;
using UnityEngine;

namespace SouthBasement.Helpers
{
    public static class FacingDirectionsHelper
    {
        public static FacingDirections GetFacingDirectionTo(Transform center, Transform target)
        {
            if (center.position.x < target.position.x)
                return FacingDirections.Right;

            return FacingDirections.Left;
        }
    }
}