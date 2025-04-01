using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IXpBarWindow> OnExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> OnWindowMoved { get; } = new();
    public AddonEvent<IAddonPoolBar> OnOffensiveTargetReady {  get; } = new();
    public AddonEvent<IAddonPoolBar> OnDefensiveTargetReady { get; } = new();
    public AddonEvent<float> OnOffTargetPoolbarChange { get; } = new();
    public AddonEvent<float> OnDefTargetPoolbarChange { get; } = new();
}