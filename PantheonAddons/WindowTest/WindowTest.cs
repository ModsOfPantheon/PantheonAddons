using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;

namespace PantheonAddons.WindowTest;

[AddonMetadata(nameof(WindowTest), "Test", "Test window stuff")]
public class WindowTest : Addon
{
    public override void OnCreate()
    {
        LocalPlayerEvents.LocalPlayerEntered.Subscribe(PlayerEntered);       
    }

    private void PlayerEntered(IPlayer obj)
    {
        var window = CustomUI.CreateWindow("Test", 400, 500);

        window.AddResizeHandle(1000, 1000, 400, 500);

        window.AddVerticalLayout();
    }

    public override void Enable()
    {
        
    }

    public override void Disable()
    {
        
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        
    }
}