using PantheonAddonFramework.UI;
using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IXpBarWindow> OnExperienceBarReady { get; }
    AddonEvent<IAddonWindow> OnWindowMoved { get; }
    AddonEvent<IAddonPoolBar> OnOffensiveTargetReady { get; }
    AddonEvent<IAddonPoolBar> OnDefensiveTargetReady { get; }
    AddonEvent<float> OnOffTargetPoolbarChange { get; }
    AddonEvent<float> OnDefTargetPoolbarChange { get; }
}