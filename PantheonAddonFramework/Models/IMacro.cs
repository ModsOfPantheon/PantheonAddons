using Il2Cpp;

namespace PantheonAddonFramework.Models;

public interface IMacro
{
    string Name { get; }
    void Activate(bool allowCancelling = true);
}
public interface IMacroList
{
    List<UIMacroButton> Buttons { get; }
}