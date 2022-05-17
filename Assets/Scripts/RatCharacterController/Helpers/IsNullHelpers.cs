using UnityEngine;

namespace RimuruDev.Helpers
{
    public static class IsNullHelpers<T> where T : MonoBehaviour
    {
        public static T IsNullHelp(ref T checkRef, Component checkComponent)
        {
            if (checkRef == null)
            {
                checkRef = checkComponent.GetComponent<T>();

                if (checkRef == null)
                    return checkRef = GameObject.FindObjectOfType<T>();

                return checkRef;
            }

            Debug.Log($"Type({checkRef})::Component({checkComponent}) == null.");

            return default(T);
        }
    }
}