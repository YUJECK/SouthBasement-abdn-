using NTC.GlobalStateMachine;
using SouthBasement.HUD.Base;
using UnityEngine;

namespace SouthBasement.HUD
{
    public sealed class GameHUD : StateMachineUser
    {
        [SerializeField] private Window _minimap; 
        [SerializeField] private Window _healthBar; 
        [SerializeField] private Window _permanentInventory; 
        [SerializeField] private Window _stamina; 
        [SerializeField] private Window _cheese;
        
        [SerializeField] private Window _restart;

        protected override void OnFight()
        {
            _minimap.Close();
        }

        protected override void OnGameIdle()
        {
            _minimap.Open();
            _healthBar.Open();
            _permanentInventory.Open();
            _stamina.Open();
            _cheese.Open();
            
            _restart.Close();
        }

        protected override void OnDied()
        {
            _minimap.Close();
            _healthBar.Close();
            _permanentInventory.Close();
            _stamina.Close();
            _cheese.Close();
            
            _restart.Open();
        }
    }
}