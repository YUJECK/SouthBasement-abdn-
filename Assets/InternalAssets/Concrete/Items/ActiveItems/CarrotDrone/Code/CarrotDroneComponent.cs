using UnityEngine;

namespace SouthBasement
{
    public abstract class CarrotDroneComponent : MonoBehaviour
    {
        protected CarrotDroneConfig CarrotDroneConfig { get; private set; }

        public void SetConfig(CarrotDroneConfig carrotDroneConfig)
        {
            CarrotDroneConfig = carrotDroneConfig;
        }
    }
}