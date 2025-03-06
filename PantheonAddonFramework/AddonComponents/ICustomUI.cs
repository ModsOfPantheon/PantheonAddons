using PantheonAddonFramework.UI;

namespace PantheonAddonFramework.AddonComponents;

public interface ICustomUI
{
    public IAddonWindow CreateWindow(string name, int initialWidth, int initialHeight);
}