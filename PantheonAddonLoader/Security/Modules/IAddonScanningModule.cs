using System.Reflection;
using Mono.Cecil;

namespace PantheonAddonLoader.Security.Modules;

public interface IAddonScanningModule
{
    bool PassesChecks(AssemblyDefinition assemblyDefinition);
}