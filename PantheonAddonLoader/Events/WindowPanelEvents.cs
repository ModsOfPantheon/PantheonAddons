using PantheonAddonFramework.Events;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.Events;

public class WindowPanelEvents : IWindowPanelEvents
{
    public AddonEvent<IXpBarWindow> ExperienceBarReady { get; } = new();
    public AddonEvent<IAddonWindow> WindowMoved { get; } = new();
}