using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;

namespace PantheonAddons.TooltipTest;

[AddonMetadata("Tooltip Test", "ModsOfPantheon", "Testing tooltips")]
public sealed class TooltipTest : Addon
{
    public override void OnCreate()
    {
        LifecycleEvents.OnUpdate.Subscribe(OnUpdate);
    }

    public override void Enable()
    {
        LifecycleEvents.OnUpdate.Subscribe(OnUpdate);
    }

    public override void Disable()
    {
        LifecycleEvents.OnUpdate.Unsubscribe(OnUpdate);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        LifecycleEvents.OnUpdate.Unsubscribe(OnUpdate);
    }
    
    private void OnUpdate()
    {
        if (Keyboard.IsKeyDown(KeyCodes.F1))
        {
            Tooltips.ShowPlain("This is a title", "This is a body");
        }

        if (Keyboard.IsKeyDown(KeyCodes.F2))
        {
            Tooltips.HidePlain();
        }
    }
}