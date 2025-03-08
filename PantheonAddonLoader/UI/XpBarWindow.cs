using Il2Cpp;
using PantheonAddonFramework.UI;

namespace PantheonAddonLoader.UI;

public class XpBarWindow : AddonWindow, IXpBarWindow
{
    private UIWindowPanel _uiWindowPanel;
    
    public XpBarWindow(UIWindowPanel window) : base(window)
    {
        _uiWindowPanel = window;
    }

    public void ShowTicks(bool show)
    {
        var ticksHolder = _uiWindowPanel.transform.GetChild(2);
        ticksHolder.gameObject.SetActive(show);
    }
}