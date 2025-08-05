using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;
using System.Xml.Linq;

namespace PantheonAddons.EnhancedBars;

[AddonMetadata("Enhanced Pool Bars", "xowis", "Makes the pools better")]
public sealed class EnhancedBars : Addon
{
    private IAddonPoolBar? _offWindowPoolbar;
    private IAddonTextComponent? _offWindowPoolbarText;
    private IAddonPoolBar? _defWindowPoolbar;
    private IAddonTextComponent? _defWindowPoolbarText;

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
        _offWindowPoolbarText?.Enable(true);
        _defWindowPoolbarText?.Enable(true);
    }

    public override void Disable()
    {
        _offWindowPoolbarText?.Enable(false);
        _defWindowPoolbarText?.Enable(false);
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
        _offWindowPoolbarText?.SetFontSize(obj);
        _defWindowPoolbarText?.SetFontSize(obj);
    }

    public override void Dispose()
    {
        // Offensive Target Window
        WindowPanelEvents.OffensiveTargetReady.Unsubscribe(OffensiveTargetReady);
        WindowPanelEvents.OffTargetPoolbarChange.Unsubscribe(HandleOffensiveTargetPoolbar);
        LocalPlayerEvents.OffensiveTargetChanged.Unsubscribe(HandleOffensiveTargetPoolbar);
        _offWindowPoolbarText?.Destroy();

        // Defensive Target Window
        WindowPanelEvents.DefensiveTargetReady.Unsubscribe(DefensiveTargetReady);
        WindowPanelEvents.DefTargetPoolbarChange.Unsubscribe(HandleDefensiveTargetPoolbar);
        LocalPlayerEvents.DefensiveTargetChanged.Unsubscribe(HandleDefensiveTargetPoolbar);
        _defWindowPoolbarText?.Destroy();
    }

    private void OffensiveTargetReady(IAddonPoolBar poolbar)
    {
        _offWindowPoolbar = poolbar;
        SetupWindow(_offWindowPoolbar);
        _offWindowPoolbarText = _offWindowPoolbar?.AddTextComponent("");
        _offWindowPoolbarText?.SetSize(500, 20);
        _offWindowPoolbarText?.SetFontSize(18);
    }

    private void HandleOffensiveTargetPoolbar(float percent)
    {
        _offWindowPoolbarText?.SetText(CreateText(percent));
    }

    private void DefensiveTargetReady(IAddonPoolBar poolbar)
    {
        _defWindowPoolbar = poolbar;
        SetupWindow(_defWindowPoolbar);
        _defWindowPoolbarText = _defWindowPoolbar?.AddTextComponent("");
        _defWindowPoolbarText?.SetSize(500, 20);
        _defWindowPoolbarText?.SetFontSize(18);
    }

    private void HandleDefensiveTargetPoolbar(float percent)
    {
        _defWindowPoolbarText?.SetText(CreateText(percent));
    }

    private static string CreateText(float percent)
    {
        return $"{percent:F0}%";
    }
    
        
    private static void SetupWindow(IAddonPoolBar poolbar)
    {
        poolbar.Bar.SetSize(0, 10);
        
        var currentSize = poolbar.Panel.GetSizeDelta();
        poolbar.Panel.SetSize(currentSize.Width, currentSize.Height + 5);

        poolbar.TargetNameText.SetAnchoredPosition(0, 20);
    }
}