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
        WindowPanelEvents.OnOffensiveTargetReady.Subscribe(OnOffensiveTargetReady);
        WindowPanelEvents.OnOffTargetPoolbarChange.Subscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OnOffensiveTargetChanged.Subscribe(HandleOffensiveTargetPoolbar);
        // Defensive Target Window
        WindowPanelEvents.OnDefensiveTargetReady.Subscribe(OnDefensiveTargetReady);
        WindowPanelEvents.OnDefTargetPoolbarChange.Subscribe(HandleDefensiveTargetPoolbar);

    }

    public override void Enable()
    {
        _OffWindowPoolbarText?.Enable(true);
    }

    public override void Disable()
    {
        _OffWindowPoolbarText?.Enable(false);
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
    }

    public override void Dispose()
    {
        // Offensive Target Window
        WindowPanelEvents.OnOffensiveTargetReady.Unsubscribe(OnOffensiveTargetReady);
        WindowPanelEvents.OnOffTargetPoolbarChange.Unsubscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OnOffensiveTargetChanged.Unsubscribe(HandleOffensiveTargetPoolbar);
        _OffWindowPoolbarText?.Destroy();

        // Defensive Target Window
        WindowPanelEvents.OnDefensiveTargetReady.Unsubscribe(OnDefensiveTargetReady);
        WindowPanelEvents.OnDefTargetPoolbarChange.Unsubscribe(HandleDefensiveTargetPoolbar);
        _DefWindowPoolbarText?.Destroy();
    }

    private void OnOffensiveTargetReady(IAddonPoolBar poolbar)
    {
        _OffWindowPoolbar = poolbar;
        _OffWindowPoolbar.SetupWindow();       
    }

    private void HandleOffensiveTargetPoolbar(float percent)
    {
        if (_OffWindowPoolbarText == null)
        {
            _OffWindowPoolbarText = _OffWindowPoolbar?.AddTextComponent("");
            _OffWindowPoolbarText?.SetSize(500, 20);
            _OffWindowPoolbarText?.SetFontSize(18);
        }

        _OffWindowPoolbarText?.SetText(CreateText(percent));
    }

    private void OnDefensiveTargetReady(IAddonPoolBar poolbar)
    {
        _DefWindowPoolbar = poolbar;
        _DefWindowPoolbar.SetupWindow();
    }

    private void HandleDefensiveTargetPoolbar(float percent)
    {
        if (_DefWindowPoolbarText == null)
        {
            _DefWindowPoolbarText = _DefWindowPoolbar?.AddTextComponent("");
            _DefWindowPoolbarText?.SetSize(500, 20);
            _DefWindowPoolbarText?.SetFontSize(18);
        }

        _DefWindowPoolbarText?.SetText(CreateText(percent));
    }

    private static string CreateText(float percent)
    {
        return $"{percent:F0}%";
    }
}