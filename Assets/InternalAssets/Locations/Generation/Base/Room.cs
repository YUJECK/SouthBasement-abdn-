using SouthBasement.Characters;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(PassageHandler))]
    public abstract class Room : MonoBehaviour
    {
        [field: SerializeField] public Vector2 RoomSize { get; private set; }
        [SerializeField] private PlayerEnterTrigger _playerEnterTrigger;

        public PassageHandler PassageHandler { get; private set; }

        public Vector2 GetOffCenter(Direction direction)
        {
            if(PassageHandler == null) 
                InitPassageHandler();

            return PassageHandler.GetPassage(direction).Factory.transform.localPosition;
        }

        private void InitPassageHandler()
        {
            PassageHandler = GetComponent<PassageHandler>();
        }

        private void Awake()
        {
            InitPassageHandler();
            BindPlayerEnterTrigger();

            OnAwake();
        }

        private void BindPlayerEnterTrigger()
        {
            if (_playerEnterTrigger == null)
                _playerEnterTrigger = GetComponentInChildren<PlayerEnterTrigger>();

            _playerEnterTrigger.OnEntered += OnPlayerEntered;
            _playerEnterTrigger.OnExit += OnPlayerExit;
        }

        protected virtual void OnAwake() { }

        protected virtual void OnPlayerEntered(Character player) { }
        protected virtual void OnPlayerExit(Character player) { }

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