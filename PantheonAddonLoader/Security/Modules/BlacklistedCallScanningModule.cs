using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PantheonAddonLoader.Security.Modules;

public class BlacklistedCallScanningModule : IAddonScanningModule
{
    private readonly string[] _blacklistedLibraries = {
        "System.IO",
        "System.Net",
        "System.Reflection"
    };

    public bool PassesChecks(AssemblyDefinition assemblyDefinition)
    {
        return assemblyDefinition.Modules.All(AnalyzeModule);
    }

    private bool AnalyzeModule(ModuleDefinition module)
    {
        return module.Types.All(AnalyzeType);
    }

    private bool AnalyzeType(TypeDefinition type)
    {
        return type.Methods.All(AnalyzeMethod);
    }

    private bool AnalyzeMethod(MethodDefinition method)
    {
        if (!method.HasBody)
        {
            return true;
        }
        
        foreach (var instruction in method.Body.Instructions)
        {
            if (instruction.OpCode != OpCodes.Call && instruction.OpCode != OpCodes.Callvirt)
            {
                continue;
            }

            if (instruction.Operand is not MethodReference calledMethod)
            {
                continue;
            }
            
            var methodNamespace = calledMethod.DeclaringType.Namespace;
            if (methodNamespace == null)
            {
                return true;
            }

            var matchingBlacklistedNamespace = _blacklistedLibraries.FirstOrDefault(x => methodNamespace.StartsWith(x));
            if (matchingBlacklistedNamespace != null)
            {
                Console.WriteLine($"Call contained blacklisted namespace {matchingBlacklistedNamespace}");
                return false;
            }
        }
        
        return true;
    }
}