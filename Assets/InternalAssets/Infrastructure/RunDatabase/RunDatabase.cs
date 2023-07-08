using System.Collections.Generic;
using SouthBasement.Economy;
using SouthBasement.InputServices;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class RunDatabase
    {
        private List<IRunDatabase> _runDatabases;
        
        private readonly DiContainer _diContainer;
        private readonly IInputService _inputService;
        
        [Inject]
        public RunDatabase(DiContainer diContainer, IInputService inputService)
        {
            _diContainer = diContainer;
            _inputService = inputService;
            
            Create();
        }

        private void Create()
        {
            var stats = new StatsDatabase(_diContainer);
            stats.Create();
            
            _runDatabases = new List<IRunDatabase>
            {
                stats,
                new ItemsAndInventoryDatabase(_diContainer, stats.Stats.AttackStats, _inputService),
                new CheeseRunDatabase(), 
            };

            foreach (var runDatabase in _runDatabases)
                runDatabase.Create();
        }

        public void Reset()
        {
            foreach (var database in _runDatabases)
                database.Reset();
        }
    }
}