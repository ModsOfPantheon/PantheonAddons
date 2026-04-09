using Il2Cpp;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

internal class InventoryItem : IInventoryItem
{
    private readonly Item _item;
    
    public InventoryItem(Item item)
    {
        _item = item;
    }

    public Guid Id => Guid.Parse(_item.ItemInstanceGuid.ToString());
    public string Name => _item.Template.ItemName;
}