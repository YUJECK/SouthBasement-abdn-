using NTC.GlobalStateMachine;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.CameraHandl;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterOnDie : StateMachineUser
    {
        private Character _character;
        private CameraHandler _cameraHandler;

        [Inject]
        private void Construct(Character characterFactory, CameraHandler cameraHandler)
        {
            _character = characterFactory;
            _cameraHandler = cameraHandler;
        }
        
        protected override void OnDied()
        {
            _character.Components.Get<PlayerAnimator>().PlayDead();
            
            _character.Components
                .Remove<ICharacterAttacker>()
                .Remove<IDashable>()
                .Remove<IFlipper>()
                .Remove<ICharacterMovable>();

            _character.GameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}