using UnityEngine;

namespace SouthBasement
{
    public sealed class CarrotDrone : MonoBehaviour
    {
        private CarrotDroneConfig _carrotDroneConfig;

        public void SetConfig(CarrotDroneConfig carrotDroneConfig)
        {
            _carrotDroneConfig = carrotDroneConfig;

            var components = GetComponentsInChildren<CarrotDroneComponent>();

            foreach (var component in components)
                component.SetConfig(carrotDroneConfig);
        }
    }
}