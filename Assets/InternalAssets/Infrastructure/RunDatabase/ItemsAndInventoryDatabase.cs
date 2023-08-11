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

        public void Remove()
        {
            Created = false;
            
            _container.Unbind<Inventory>();
            _container.Unbind<ActiveItemUsage>();
            _container.Unbind<WeaponsUsage>();
            _container.Unbind<PassiveItemsUsage>();
        }

        public void Reset()
        {
            if (!Created) Create();

            _coroutineRunner = _container.Resolve<ICoroutineRunner>();
            
            _inventoryDatabase.Reset();

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
                .FromInstance(_inventory);

        }
        private void BindActiveItemUsager()
        {
            _container
                .Bind<ActiveItemUsage>()
                .FromInstance(new ActiveItemUsage(_inputService, _inventory));
        }
        private void BindWeaponsItemUsager()
        {
            _weaponsUsage = new WeaponsUsage(_inventory, _attackStats);

            _container
                .Bind<WeaponsUsage>()
                .FromInstance(_weaponsUsage);
        }
        private void BindPassiveItemUsager()
        {
            _container
                .BindInterfacesTo<PassiveItemsUsage>()
                .FromInstance(new PassiveItemsUsage(_inventory));
        }

        public void OnCharacterSpawned()
        {
            if(_weaponsUsage.CurrentWeapon != null)
                _weaponsUsage.CurrentWeapon.OnEquip();
        }
    }
}