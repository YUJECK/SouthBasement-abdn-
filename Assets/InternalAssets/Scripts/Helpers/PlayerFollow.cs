using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public class PlayerFollow : MonoBehaviour
    {
        private Transform _player;

        [Inject]
        private void Construct(Character character) => _player = character.transform;

        private void LateUpdate() => transform.position = new Vector3(_player.position.x, _player.position.y, -100f);
    }
}
