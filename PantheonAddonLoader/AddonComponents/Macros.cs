using Il2Cpp;
using PantheonAddonFramework.AddonComponents;
using PantheonAddonFramework.Models;
using PantheonAddonLoader.Models;

namespace PantheonAddonLoader.AddonComponents;

public class Macros : IMacros
{
    private readonly List<IMacro> _macros = new List<IMacro>();

    public IMacro? GetByName(string name)
    {
        var macroBar = UIMacroBar.Instance;
        if (macroBar is null)
        {
            return null;
        }

        var window = macroBar.windowPanel;
        if (!window.IsVisible)
        {
            return null;
        }

        var buttonRoot = macroBar.ButtonRoot;

        var macroButtons = buttonRoot.GetComponentsInChildren<UIMacroButton>();
        var match = macroButtons.FirstOrDefault(m => m.Name == name);

        return match is null ? null : new Macro(match);
    }
    public IMacro[]? GetAllMacros()
    {
        return _macros.Count > 0 ? _macros.ToArray() : null;
    }
}
