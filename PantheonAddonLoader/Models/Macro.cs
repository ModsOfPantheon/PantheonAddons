using Il2Cpp;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

public class Macro : IMacro
{
    private readonly UIMacroButton _macroButton;

    public Macro(UIMacroButton macroButton)
    {
        _macroButton = macroButton;
    }

    public string Name => _macroButton.Name;
    
    public void Activate(bool allowCancelling)
    {
        var macroBar = UIMacroBar.Instance;
        if (macroBar is null)
        {
            return;
        }

        var window = macroBar.windowPanel;
        if (!window.IsVisible)
        {
            return;
        }

        if (_macroButton.Busy && !allowCancelling)
        {
            return;
        }
        
        _macroButton.OnClick();
    }
}
