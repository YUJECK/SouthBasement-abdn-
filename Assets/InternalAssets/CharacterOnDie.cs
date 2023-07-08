using NTC.GlobalStateMachine;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Stats;
using SouthBasement.CameraHandl;
using TheRat.InternalAssets.Characters.Base;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterOnDie : StateMachineUser
    {
        private Character _character;
        private CameraHandler _cameraHandler;

        [Inject]
        private void Construct(CharacterFactory characterFactory, CharacterHealthStats characterHealthStats, CameraHandler cameraHandler)
        {
            _character = characterFactory.Instance;
            _cameraHandler = cameraHandler;
        }


        protected override void OnDied()
        {
            _character.Components.Get<PlayerAnimator>().PlayDead();
            
            _character.Components
                .Remove<IAttacker>()
                .Remove<ICharacterMovable>()
                .Remove<IDashable>()
                .Remove<IFlipper>();
        }
    }
}