using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddons.EnhancedExperienceBar;

[AddonMetadata("Enhanced Experience Bar", "Roast", "Makes the experience bar taller")]
public sealed class EnhancedExperienceBar : Addon
{
    private float _originalHeight;
    private float _originalWidth;
    private IAddonWindow? _xpWindow;
    private IAddonTextComponent? _xpText;
    private IPlayer? _player;

    public override void OnCreate()
    {
        WindowPanelEvents.OnExperienceBarReady.Subscribe(OnExperienceBarReady);
        LocalPlayerEvents.OnExperienceChanged.Subscribe(OnExperienceChanged);
        LocalPlayerEvents.OnLocalPlayerEntered.Subscribe(OnPlayerEntered);
    }

    public override void Enable()
    {
        _xpWindow?.SetHeight(_xpWindow.Height + 10);
        _xpWindow?.SetWidth(_xpWindow.Width / 2);
        var experience = _player?.GetExperience();

        if (experience == null)
        {
            return;
        }
        _xpText?.SetText(CreateText(experience));
    }

    public override void Disable()
    {
        _xpWindow?.SetHeight(_originalHeight);
        _xpWindow?.SetWidth(_originalWidth);
        _xpText?.SetText("");
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
        WindowPanelEvents.OnExperienceBarReady.Unsubscribe(OnExperienceBarReady);
        LocalPlayerEvents.OnExperienceChanged.Unsubscribe(OnExperienceChanged);
    }

    private void OnPlayerEntered(IPlayer player)
    {
        if (!player.IsLocalPlayer)
        {
            return;
        }

        _player = player;

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
        _originalWidth = window.Width;
        _xpWindow = window;

        _xpWindow.SetHeight(_originalHeight + 10);
        _xpWindow.SetWidth(_originalWidth / 2);

        _xpText = _xpWindow.AddTextComponent("0 / 0");
        _xpText.SetSize(500, 20);
    }

    private static string CreateText(PlayerExperience playerExperience)
    {
        return $"{playerExperience.Current:N0} / {playerExperience.ToNextLevel:N0} ({playerExperience.ExperiencePercentage * 100:F}%)";
    }
}