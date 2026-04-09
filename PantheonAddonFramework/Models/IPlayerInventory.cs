using PantheonAddonFramework.Events;

namespace PantheonAddonFramework.Models;

public interface IPlayerInventory
{
    IEnumerable<IInventoryItem> Items { get; }
}