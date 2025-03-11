using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.AddonComponents;

public interface IMacros
{
    IMacro? GetByName(string name);
    IEnumerable<IMacro> GetAll();
}
