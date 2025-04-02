using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.UI;

namespace PantheonAddons.SnapUIToGrid;

[AddonMetadata("Snap UI to grid", "Roast", "Snaps windows to a grid when moving")]
public sealed class SnapUIToGrid : Addon
{
    private bool _isSnapping;
    private float _snapAmount = 5.0f;
    
    public override void OnCreate()
    {
        WindowPanelEvents.WindowMoved.Subscribe(OnWindowMoved);
    }

    private void OnWindowMoved(IAddonWindow window)
    {
        if (!_isSnapping)
        {
            return;
        }
        
        var windowX = window.X;
        var windowY = window.Y;

        var newX = GetNearestMultiple(windowX, _snapAmount);
        var newY = GetNearestMultiple(windowY, _snapAmount);
        
        window.SetPosition(newX, newY);
    }

    public override void Enable()
    {
        _isSnapping = true;
    }

    public override void Disable()
    {
        _isSnapping = false;
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return new IConfigurationValue[]
        {
            new FloatConfigurationValue("Snap Amount", "The amount in game units to snap UI to. Larger value means bigger snap movements.", 5.0f, 1.0f, 10.0f, 0.1f, f => _snapAmount = f)
        };
    }

    public override void Dispose()
    {
        WindowPanelEvents.WindowMoved.Unsubscribe(OnWindowMoved);
    }
    
    private static float GetNearestMultiple(float number, float multiple)
    {
        return (float)(Math.Round(number / multiple) * multiple);
    }
}