﻿using System.Collections.Generic;
using SouthBasement.Characters.Base;
using SouthBasement.InputServices;
using SouthBasement.Items.Weapons;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class RunDatabase
    {
        private List<IRunDatabase> _runDatabases;
        
        private readonly DiContainer _diContainer;
        private readonly IInputService _inputService;
        private CharacterRunDatabase _characterRunDatabase;

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

            _characterRunDatabase = new CharacterRunDatabase(_diContainer);
            
            _runDatabases = new List<IRunDatabase>
            {
                stats,
                new ItemsAndInventoryDatabase(_diContainer, stats.Stats.CombatStats, _inputService),
                new CheeseRunDatabase(),
                _characterRunDatabase, 
            };

            foreach (var runDatabase in _runDatabases)
                runDatabase.Create();
        }

        public void Remove()
        {
            foreach (var database in _runDatabases)
                database.Remove();    
            
            WeaponsStatsMultiplier.ResetAll();
        }
        
        public void Reset()
        {
            foreach (var database in _runDatabases)
                database.Reset();

            WeaponsStatsMultiplier.ResetAll();
        }

        public void OnCharacterSpawned(CharacterGameObject characterGameObject)
        {
            _characterRunDatabase.Character.OnCharacterPrefabSpawned(characterGameObject);

            foreach (var runDatabase in _runDatabases)
                runDatabase.OnCharacterSpawned();
        }
    }
}