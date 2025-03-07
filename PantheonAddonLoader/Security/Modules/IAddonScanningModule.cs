using System.Reflection;

namespace PantheonAddonLoader.Security.Modules;

public interface IAddonScanningModule
{
    bool PassesChecks(Assembly assembly, SafeLoadContext safeLoadContext);
}