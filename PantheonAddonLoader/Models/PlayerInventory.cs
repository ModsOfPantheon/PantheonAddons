using Il2Cpp;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

public class PlayerInventory : IPlayerInventory
{
    private readonly InventoryWithPersyst.Logic _inventory;
    
    public PlayerInventory(InventoryWithPersyst.Logic inventory)
    {
        _inventory = inventory;
    }

    public IEnumerable<IInventoryItem> Items => MapItems();

    private IEnumerable<IInventoryItem> MapItems()
    {
        var items = new List<IInventoryItem>();
        
        foreach (var item in _inventory.items)
        {
            items.Add(new InventoryItem(item.Value));
        }

        return items;
    }
}