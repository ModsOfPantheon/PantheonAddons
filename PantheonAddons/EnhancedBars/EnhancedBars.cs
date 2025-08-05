using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;
using System.Xml.Linq;

namespace PantheonAddons.EnhancedBars;

[AddonMetadata("Enhanced Pool Bars", "xowis", "Makes the pools better")]
public sealed class EnhancedBars : Addon
{
    private IAddonPoolBar? _OffWindowPoolbar;
    private IAddonTextComponent? _OffWindowPoolbarText;
    private IAddonPoolBar? _DefWindowPoolbar;
    private IAddonTextComponent? _DefWindowPoolbarText;

    public override void OnCreate()
    {
        // Offensive Target Window
        WindowPanelEvents.OffensiveTargetReady.Subscribe(OffensiveTargetReady);
        WindowPanelEvents.OffTargetPoolbarChange.Subscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OffensiveTargetChanged.Subscribe(HandleOffensiveTargetPoolbar);
        // Defensive Target Window
        WindowPanelEvents.DefensiveTargetReady.Subscribe(DefensiveTargetReady);
        WindowPanelEvents.DefTargetPoolbarChange.Subscribe(HandleDefensiveTargetPoolbar);
        LocalPlayerEvents.DefensiveTargetChanged.Subscribe(HandleDefensiveTargetPoolbar);
    }

    public override void Enable()
    {
        _OffWindowPoolbarText?.Enable(true);
        _DefWindowPoolbarText?.Enable(true);
    }

    public override void Disable()
    {
        _OffWindowPoolbarText?.Enable(false);
        _DefWindowPoolbarText?.Enable(false);
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return new IConfigurationValue[]
        {
            new FloatConfigurationValue("Set Font Size", "Sets the font size for the pool bar overlays.", 18.0f, 10.0f, 72.0f, 1.0f, OnFontSizeChanged)
        };
    }

    private void OnFontSizeChanged(float obj)
    {
        _OffWindowPoolbarText?.SetFontSize(obj);
        _DefWindowPoolbarText?.SetFontSize(obj);
    }

    public override void Dispose()
    {
        // Offensive Target Window
        WindowPanelEvents.OffensiveTargetReady.Unsubscribe(OffensiveTargetReady);
        WindowPanelEvents.OffTargetPoolbarChange.Unsubscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OffensiveTargetChanged.Unsubscribe(HandleOffensiveTargetPoolbar);
        _OffWindowPoolbarText?.Destroy();

        // Defensive Target Window
        WindowPanelEvents.DefensiveTargetReady.Unsubscribe(DefensiveTargetReady);
        WindowPanelEvents.DefTargetPoolbarChange.Unsubscribe(HandleDefensiveTargetPoolbar);
        LocalPlayerEvents.DefensiveTargetChanged.Unsubscribe(HandleDefensiveTargetPoolbar);
        _DefWindowPoolbarText?.Destroy();
    }

    private void OffensiveTargetReady(IAddonPoolBar poolbar)
    {
        _OffWindowPoolbar = poolbar;
        _OffWindowPoolbar.SetupWindow();
        _OffWindowPoolbarText = _OffWindowPoolbar?.AddTextComponent("");
        _OffWindowPoolbarText?.SetSize(500, 20);
        _OffWindowPoolbarText?.SetFontSize(18);
    }

    private void HandleOffensiveTargetPoolbar(float percent)
    {
        _OffWindowPoolbarText?.SetText(CreateText(percent));
    }

    private void DefensiveTargetReady(IAddonPoolBar poolbar)
    {
        _DefWindowPoolbar = poolbar;
        _DefWindowPoolbar.SetupWindow();
        _DefWindowPoolbarText = _DefWindowPoolbar?.AddTextComponent("");
        _DefWindowPoolbarText?.SetSize(500, 20);
        _DefWindowPoolbarText?.SetFontSize(18);
    }

    private void HandleDefensiveTargetPoolbar(float percent)
    {
        _DefWindowPoolbarText?.SetText(CreateText(percent));
    }

    private static string CreateText(float percent)
    {
        return $"{percent:F0}%";
    }
}