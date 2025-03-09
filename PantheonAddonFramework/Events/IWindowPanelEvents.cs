using PantheonAddonFramework.UI;

namespace PantheonAddonFramework.Events;

public interface IWindowPanelEvents
{
    AddonEvent<IXpBarWindow> OnExperienceBarReady { get; }
    AddonEvent<IAddonWindow> OnWindowMoved { get; }
}