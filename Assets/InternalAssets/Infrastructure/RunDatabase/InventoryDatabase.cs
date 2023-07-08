using SouthBasement.InventorySystem;

namespace SouthBasement
{
    public sealed class InventoryDatabase : IRunDatabase
    {
        public Inventory Inventory { get; private set; }

        public bool Created { get; private set; }

        public void Create()
        {
            if (Created) 
                return;
            
            Inventory = new Inventory();
            
            CreateInventoryContainers();
            
            Created = true;
        }

        private void CreateInventoryContainers()
        {
            Inventory.ItemsContainer
                .AddContainer<JunkItem>(new StackableTypeContainer(), 12)
                .AddContainer<FoodItem>(new TypeContainer(), 6)
                .AddContainer<ActiveItem>(new TypeContainer(), 2)
                .AddContainer<PassiveItem>(new TypeContainer(), 24)
                .AddContainer<WeaponItem>(new TypeContainer(), 3);
        }

        public void Reset()
        {
            Inventory.ItemsContainer.Clear();
            CreateInventoryContainers();

            Inventory.ItemsContainer.GetAllInContainer<FoodItem>();
        }
    }
}