using System;
using SouthBasement.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.InternalAssets.HUD
{
    [RequireComponent(typeof(Button))]
    public sealed class RestartButton : MonoBehaviour
    {
        private RunStarter _runStarter;

        [Inject]
        private void Construct(RunStarter runStarter)
        {
            _runStarter = runStarter;
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            _runStarter.StartRun();
        }
    }
}