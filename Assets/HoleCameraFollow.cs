using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Hole;
using UnityEngine;

namespace SouthBasement
{
    public class HoleCameraFollow : MonoBehaviour
    {
        private Transform _followTarget;
        
        private void Awake()
        {
            FindObjectOfType<CharacterChoosingService>().OnCharacterChosen += EnableFollow;
        }

        private void EnableFollow(CharacterDummy dummy)
            => _followTarget = dummy.transform;

        private void LateUpdate()
        {
            if(_followTarget == null)
                return;
     
            transform.position = new Vector3(_followTarget.transform.position.x, _followTarget.position.y, -100f);
        }
    }
}
