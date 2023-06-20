using UnityEngine;

namespace SouthBasement.HUD.Base
{
    public abstract class Window : MonoBehaviour
    {
        public abstract void Open();
        public abstract void Close();
    }
}