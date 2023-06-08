using TheRat.Player;
using UnityEngine;

namespace VioletHell.PlayerServices
{
    public class PlayerFlipper : MonoBehaviour
    {
        private bool _faceRight = true;

        private void Start()
            => GetComponent<Character>().Movable.OnMoved += OnMoved;

        private void OnMoved(Vector2 obj)
        {
            if(obj.x > 0 && !_faceRight)
            {
                transform.Rotate(0f, 180f, 0f);
                _faceRight = true;
            }
            else if (obj.x < 0 && _faceRight)
            {
                transform.Rotate(0f, -180f, 0f);
                _faceRight = false;
            }
        }
    }
}
