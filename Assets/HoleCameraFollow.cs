using System;
using SouthBasement.Characters;
using UnityEngine;

namespace TheRat
{
    public class HoleCameraFollow : MonoBehaviour
    {
        private Transform _followTarget;
        
        private void Awake()
        {
            FindObjectOfType<CharacterChoosingService>().OnCharacterChosen += EnableFollow;
        }

        private void EnableFollow(Character character)
            => _followTarget = character.transform;

        private void LateUpdate()
        {
            if(_followTarget == null)
                return;
     
            transform.position = new Vector3(_followTarget.transform.position.x, _followTarget.position.y, -100f);
        }
    }
}
