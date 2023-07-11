using SouthBasement.Characters;
using SouthBasement.Characters.Base;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(PassageHandler))]
    public abstract class Room : MonoBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; private set; }
        [field: SerializeField] public PassageHandler PassageHandler { get; private set; }

        public PlayerEnterTrigger PlayerEnterTrigger { get; private set; }

        public Vector2 GetOffCenter(Direction direction)
        {
            if(PassageHandler == null) 
                InitPassageHandler();

            return PassageHandler.GetPassage(direction).Factory.transform.localPosition;
        }

        private void InitPassageHandler() => PassageHandler = GetComponent<PassageHandler>();

        private void Reset() => InitPassageHandler();

        private void Awake()
        {
            BindPlayerEnterTrigger();
            OnAwake();
        }

        private void BindPlayerEnterTrigger()
        {
            if (PlayerEnterTrigger == null)
                PlayerEnterTrigger = GetComponentInChildren<PlayerEnterTrigger>();

            PlayerEnterTrigger.OnEntered += OnPlayerEntered;
            PlayerEnterTrigger.OnExit += OnPlayerExit;
        }

        protected virtual void OnAwake() { }

        protected virtual void OnPlayerEntered(CharacterGameObject player) { }
        protected virtual void OnPlayerExit(CharacterGameObject player) { }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (TryGetComponent(out BoxCollider2D boxCollider2D))
                boxCollider2D.size = RoomSize;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, RoomSize);
        }
#endif
    }
}