using Il2Cpp;
using PantheonAddonFramework.AddonComponents;

namespace PantheonAddonLoader.AddonComponents;

public class Tooltips : ITooltips
{
    public void ShowPlain(string header, string body)
    {
        UITooltipPanel.Instance.ShowTooltip(header, body, null);
    }

    public void HidePlain()
    {
        UITooltipPanel.Instance.HideTooltip();
    }
}