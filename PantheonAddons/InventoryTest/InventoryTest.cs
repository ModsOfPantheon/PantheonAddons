using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;

namespace PantheonAddons.InventoryTest;

[AddonMetadata(nameof(InventoryTest), "Test", "Test inventory API")]
public class InventoryTest : Addon
{
    public override void OnCreate()
    {
        LocalPlayerEvents.ItemAdded.Subscribe(ItemAdded);
        LocalPlayerEvents.ItemRemoved.Subscribe(ItemRemoved);
    }

    public override void Enable()
    {
        LocalPlayerEvents.ItemAdded.Subscribe(ItemAdded);
        LocalPlayerEvents.ItemRemoved.Subscribe(ItemRemoved);
    }

    public override void Disable()
    {
        LocalPlayerEvents.ItemAdded.Unsubscribe(ItemAdded);
        LocalPlayerEvents.ItemRemoved.Unsubscribe(ItemRemoved);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        LocalPlayerEvents.ItemAdded.Unsubscribe(ItemAdded);
        LocalPlayerEvents.ItemRemoved.Unsubscribe(ItemRemoved);
    }
    
    private void ItemRemoved(IInventoryItem obj)
    {
        Logger.Info($"Item removed {obj.Name}");
    }

    private void ItemAdded(IInventoryItem obj)
    {
        Logger.Info($"Item added {obj.Name}");
    }
}