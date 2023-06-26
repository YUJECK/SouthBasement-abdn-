using NTC.GlobalStateMachine;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Stats;
using SouthBasement.CameraHandl;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterOnDie : StateMachineUser
    {
        private Character _character;
        private CameraHandler _cameraHandler;

        [Inject]
        private void Construct(Character character, CharacterHealthStats characterHealthStats, CameraHandler cameraHandler)
        {
            _character = character;
            _cameraHandler = cameraHandler;
        }


        protected override void OnDied()
        {
            _cameraHandler.SwitchTo(CameraTags.Death);

            _character.Components.Get<PlayerAnimator>().PlayDead();
            
            _character.Components
                .Remove<IAttackable>()
                .Remove<IMovable>()
                .Remove<IDashable>()
                .Remove<IFlipper>();
        }
    }
}