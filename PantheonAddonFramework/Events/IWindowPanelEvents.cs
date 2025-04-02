using PantheonAddonFramework.UI;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IXpBarWindow> ExperienceBarReady { get; }
    AddonEvent<IAddonWindow> WindowMoved { get; }
}