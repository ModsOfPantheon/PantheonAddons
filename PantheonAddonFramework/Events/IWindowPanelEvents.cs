using PantheonAddonFramework.UI;
using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IXpBarWindow> ExperienceBarReady { get; }
    AddonEvent<IAddonWindow> WindowMoved { get; }
    AddonEvent<IAddonPoolBar> OffensiveTargetReady { get; }
    AddonEvent<IAddonPoolBar> DefensiveTargetReady { get; }
    AddonEvent<float> OffTargetPoolbarChange { get; }
    AddonEvent<float> DefTargetPoolbarChange { get; }
}