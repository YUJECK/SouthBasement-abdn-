using UnityEngine;

namespace SouthBasement
{
    public class SpawnPoint<TObject> 
        where TObject : MonoBehaviour
    {
        public readonly Transform Transform;
        public TObject Instance { get; private set; }

        public SpawnPoint(Transform transform)
        {
            this.Transform = transform;
        }

        public bool Spawned => Instance != null;

        public void SetToThis(TObject instance)
        {
            Instance = instance;

            if (Instance.transform.parent != Transform)
                Instance.transform.parent = Transform;
        }
    }
}