using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddons.EnhancedExperienceBar;

[AddonMetadata("Enhanced Experience Bar", "Roast", "Makes the experience bar taller")]
public sealed class EnhancedExperienceBar : Addon
{
    private float _originalHeight;
    private IAddonWindow? _xpWindow;
    private IAddonTextComponent? _xpText;

    public override void OnCreate()
    {
        WindowPanelEvents.OnExperienceBarReady.Subscribe(OnExperienceBarReady);
        LocalPlayerEvents.OnExperienceChanged.Subscribe(OnExperienceChanged);
        LocalPlayerEvents.OnLocalPlayerEntered.Subscribe(OnPlayerEntered);
    }

    public override void Enable()
    {
        _xpWindow?.SetHeight(_xpWindow.Height + 10);
        _xpText?.Enable(true);
    }
    
    public override void Disable()
    {
        _xpWindow?.SetHeight(_originalHeight);
        _xpText?.Enable(false);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        WindowPanelEvents.OnExperienceBarReady.Unsubscribe(OnExperienceBarReady);
        LocalPlayerEvents.OnExperienceChanged.Unsubscribe(OnExperienceChanged);
        
        _xpText?.Destroy();
    }
 
    private void OnPlayerEntered(IPlayer player)
    {
        if (!player.IsLocalPlayer)
        {
            return;
        }
        
        var experience = player.GetExperience();

        if (experience == null)
        {
            return;
        }
        _xpText?.SetText(CreateText(experience));
    }

    private void OnExperienceChanged(PlayerExperience playerExperience)
    {
        _xpText?.SetText(CreateText(playerExperience));
    }

    private void OnExperienceBarReady(IAddonWindow window)
    {
        _originalHeight = window.Height;
        _xpWindow = window;
        
        _xpWindow.SetHeight(_originalHeight + 10);

        _xpText = _xpWindow.AddTextComponent("0 / 0");
        _xpText.SetSize(500, 20);
    }
    
    private static string CreateText(PlayerExperience playerExperience)
    {
        return $"{playerExperience.Current:N0} / {playerExperience.ToNextLevel:N0} ({playerExperience.ExperiencePercentage * 100:F}%)";
    }
}