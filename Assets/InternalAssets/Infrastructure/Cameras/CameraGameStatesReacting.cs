using NTC.GlobalStateMachine;
using Zenject;

namespace SouthBasement.CameraHandl
{
    public class CameraGameStatesReacting : StateMachineUser
    {
        private CameraHandler _cameraHandler;

        [Inject]
        private void Construct(CameraHandler cameraHandler)
        {
            _cameraHandler = cameraHandler;
        }
        
        protected override void OnDied()
        {
            _cameraHandler.SwitchTo(CameraNames.Death);
        }

        protected override void OnNPC()
        {
            _cameraHandler.SwitchTo(CameraNames.NPC);
        }

        protected override void OnGameIdle()
        {
            _cameraHandler.SwitchTo(CameraNames.Main);
        }
    }
}