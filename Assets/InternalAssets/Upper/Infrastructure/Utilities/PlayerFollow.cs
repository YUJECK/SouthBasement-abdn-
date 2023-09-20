using SouthBasement.Characters.Base;
using UnityEngine;

namespace SouthBasement
{
    public class PlayerFollow : MonoBehaviour
    {
        private Transform _player;

        private void Start()
            => _player = FindObjectOfType<CharacterGameObject>().transform;

        private void LateUpdate() => transform.position = new Vector3(_player.position.x, _player.position.y, -100f);
    }
}
