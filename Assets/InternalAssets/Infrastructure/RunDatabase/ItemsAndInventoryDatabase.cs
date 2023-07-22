using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.Helpers;
using SouthBasement.Infrastucture;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class ItemsAndInventoryDatabase : IRunDatabase
    {
        private readonly DiContainer _container;
        
        private readonly CharacterAttackStats _attackStats;
        private readonly IInputService _inputService;
        private Inventory _inventory;
        private ICoroutineRunner _coroutineRunner;
        private InventoryDatabase _inventoryDatabase;
        private WeaponsUsage _weaponsUsage;

        public bool Created { get; private set; }

        public ItemsAndInventoryDatabase(DiContainer container, CharacterAttackStats attackStats, IInputService inputService)
        {
            _container = container;
            
            _attackStats = attackStats;
            _inputService = inputService;
        }

        public void Create()
        {
            if (Created) return;

            BindInventory();
            BindActiveItemUsager();
            BindPassiveItemUsager();
            BindWeaponsItemUsager();
            
            Created = true;
        }
        public void Reset()
        {
            if (!Created) Create();

            _coroutineRunner = _container.Resolve<ICoroutineRunner>();
            
            _inventoryDatabase.Reset();

            var configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = ScriptableObject.Instantiate(configPrefab);
            
            CharacterStats stats = new(config.DefaultStats);

            _container
                .Rebind<CharacterStats>()
                .FromInstance(stats);

            _container
                .Rebind<CharacterAttackStats>()
                .FromInstance(stats.AttackStats);

            _container
                .Rebind<CharacterHealthStats>()
                .FromInstance(stats.HealthStats);

            _container
                .Rebind<CharacterMoveStats>()
                .FromInstance(stats.MoveStats);

            _container
                .Rebind<CharacterStaminaStats>()
                .FromInstance(stats.StaminaStats);

            _container
                .Rebind<StaminaController>()
                .FromInstance(new StaminaController(stats.StaminaStats, _coroutineRunner));

            _container
                .Rebind<Inventory>()
                .FromInstance(_inventory);

            _container
                .Rebind<ActiveItemUsage>()
                .FromInstance(new ActiveItemUsage(_inputService, _inventory));

            _weaponsUsage = new WeaponsUsage(_inventory, _attackStats);
            
            _container
                .Rebind<WeaponsUsage>()
                .FromInstance(_weaponsUsage);

            _container
                .Rebind<PassiveItemsUsage>()
                .FromInstance(new PassiveItemsUsage(_inventory));
        }
        
        private void BindInventory()
        {
            _inventoryDatabase = new InventoryDatabase();
            
            _inventoryDatabase.Create();
            _inventory = _inventoryDatabase.Inventory;
        
            _container
                .BindInterfacesAndSelfTo<Inventory>()
                .FromInstance(_inventory)
                .AsSingle();
        }
        private void BindActiveItemUsager()
        {
            _container
                .Bind<ActiveItemUsage>()
                .FromInstance(new ActiveItemUsage(_inputService, _inventory))
                .AsSingle();    
        }
        private void BindWeaponsItemUsager()
        {
            _weaponsUsage = new WeaponsUsage(_inventory, _attackStats);
            
            _container
                .Bind<WeaponsUsage>()
                .FromInstance(_weaponsUsage)
                .AsSingle();
        }
        private void BindPassiveItemUsager()
        {
            _container
                .BindInterfacesTo<PassiveItemsUsage>()
                .FromInstance(new PassiveItemsUsage(_inventory))
                .AsSingle();
        }

        public void OnCharacterSpawned()
        {
            if(_weaponsUsage.CurrentWeapon != null)
                _weaponsUsage.CurrentWeapon.OnEquip();
        }
    }
}