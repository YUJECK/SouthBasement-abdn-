using System.Collections;
using System.Collections.Generic;
using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public class PlayerFollow : MonoBehaviour
    {
        private Transform player;

        [Inject]
        private void Construct(Character character)
        {
            player = character.transform;
        }
        
        void LateUpdate()
        {
            transform.position = new Vector3(player.position.x, player.position.y, -100f);
        }
    }
}
