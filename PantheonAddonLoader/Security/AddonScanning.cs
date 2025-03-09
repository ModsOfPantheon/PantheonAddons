using Mono.Cecil;
using PantheonAddonLoader.Security.Modules;

namespace PantheonAddonLoader.Security;

public class AddonScanning : IAddonScanning
{
    private readonly IAddonScanningModule[] _scanningModules =
    {
        new BlacklistedCallScanningModule()
    };

    public bool ShouldLoad(string addonFile)
    {
        using var assemblyDefinition = AssemblyDefinition.ReadAssembly(addonFile);

        return _scanningModules.All(x => x.PassesChecks(assemblyDefinition));
    }
}