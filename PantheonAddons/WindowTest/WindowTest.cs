using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.UI;

namespace PantheonAddons.WindowTest;

[AddonMetadata(nameof(WindowTest), "Test", "Test window stuff")]
public class WindowTest : Addon
{
    public override void OnCreate()
    {
        WindowPanelEvents.ExperienceBarReady.Subscribe(PlayerEntered);
    }

    private void PlayerEntered(IXpBarWindow xpBarWindow)
    {
        var window = CustomUI.CreateWindow("Test", 400, 500);

        window.AddResizeHandle(1000, 1000, 400, 500);

        var layout = window.AddVerticalLayout();

        layout.AddTextComponent("Hello");
        layout.AddTextComponent("World");
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