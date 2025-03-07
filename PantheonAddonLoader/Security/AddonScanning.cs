using System.Reflection;
using PantheonAddonLoader.Security.Modules;

namespace PantheonAddonLoader.Security;

public class AddonScanning : IAddonScanning
{
    private readonly IAddonScanningModule[] _scanningModules =
    {
        new SystemIOScanningModule()
    };

    public bool ShouldLoad(string addonFile)
    {
        var safeContext = new SafeLoadContext();
        var assembly = safeContext.LoadFromAssemblyPath(addonFile);

        return _scanningModules.All(module => module.PassesChecks(assembly, safeContext));
    }
}