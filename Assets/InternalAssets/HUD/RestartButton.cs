using System;
using SouthBasement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Button))]
    public sealed class RestartButton : MonoBehaviour
    {
        private RunController _runController;

        [Inject]
        private void Construct(RunController runController)
        {
            _runController = runController;
        }
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            _runController.StartRun();
        }
    }
}