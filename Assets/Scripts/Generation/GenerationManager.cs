using System.Collections.Generic;
using UnityEngine;

namespace Generation
{
    public class GenerationManager : MonoBehaviour
    {
        [Header("Настройки генерации")]
        public int roomsCount = 10;
        public int nowSpawnedRoomsCount = 0;
        public List<GameObject> rooms = new List<GameObject>();
    }
}