using System;
using TheRat.Player;
using UnityEngine;

namespace TheRat.PlayerServices
{
    public class PlayerFlipper : MonoBehaviour
    {
        private bool _faceRight = true;
        [SerializeField] private Transform target;

        private void Awake()
        {
            target = FindObjectOfType<Mouse>().transform;
        }

        private void Update()
        {
            if(target.position.x > transform.position.x && !_faceRight)
            {
                transform.Rotate(0f, 180f, 0f);
                _faceRight = true;
            }
            else if (target.position.x < transform.position.x && _faceRight)
            {
                transform.Rotate(0f, -180f, 0f);
                _faceRight = false;
            }
        }
    }
}
