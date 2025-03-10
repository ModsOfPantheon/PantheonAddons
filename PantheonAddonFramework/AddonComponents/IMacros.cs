using Il2Cpp;
using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.AddonComponents;

public interface IMacros
{
    IMacro? GetByName(string name);
}
public interface IMacroLists
{
    IMacroList? GetAllMacros();
}