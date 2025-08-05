using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IXpBarWindow> ExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> WindowMoved { get; } = new();
    public AddonEvent<IAddonPoolBar> OffensiveTargetReady { get; } = new();
    public AddonEvent<IAddonPoolBar> DefensiveTargetReady { get; } = new();
    public AddonEvent<float> OffTargetPoolbarChange { get; } = new();
    public AddonEvent<float> DefTargetPoolbarChange { get; } = new();
}