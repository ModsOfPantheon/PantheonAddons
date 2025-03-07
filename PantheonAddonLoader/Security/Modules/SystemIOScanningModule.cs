using System.Reflection;
using System.Runtime.Loader;
using MelonLoader;

namespace PantheonAddonLoader.Security.Modules;

public class SystemIOScanningModule : IAddonScanningModule
{
    private readonly HashSet<string> scannedAssemblies = new HashSet<string>();

    public bool PassesChecks(Assembly assembly, SafeLoadContext safeLoadContext)
    {
        var safeContext = new SafeLoadContext();

        try
        {
            return ScanAssemblyRecursive(assembly, safeContext);
        }
        catch
        {
            return true; // If the assembly can't be loaded, assume it's suspicious
        }
    }

    private bool ScanAssemblyRecursive(Assembly assembly, AssemblyLoadContext context)
    {
        if (!scannedAssemblies.Add(assembly.FullName))
        {
            return false; // Skip duplicates
        }

        if (ScanAssembly(assembly))
        {
            return true; // If suspicious, return immediately
        }

        foreach (var reference in assembly.GetReferencedAssemblies())
        {
            try
            {
                var refAssembly = context.LoadFromAssemblyName(reference);
                if (ScanAssemblyRecursive(refAssembly, context)) return true;
            }
            catch
            {
                return true; // If a referenced assembly can't be loaded, assume it's suspicious
            }
        }

        return false; // No suspicious activity found
    }

    private static bool ScanAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                   BindingFlags.Instance | BindingFlags.Static))
            {
                try
                {
                    var body = method.GetMethodBody();

                    var ilBytes = body?.GetILAsByteArray();
                    if (ilBytes == null) continue;

                    if (ContainsSuspiciousCall(ilBytes, assembly)) return true;
                }
                catch
                {
                    return true; // If we can't analyze a method, assume it's suspicious
                }
            }
        }

        return false;
    }

    private static bool ContainsSuspiciousCall(byte[] ilBytes, Assembly assembly)
    {
        for (var i = 0; i < ilBytes.Length; i++)
        {
            if (ilBytes[i] == 0x28 || ilBytes[i] == 0x6F) // Call or CallVirt opcode
            {
                var metadataToken = BitConverter.ToInt32(ilBytes, i + 1);

                try
                {
                    var method = assembly.ManifestModule.ResolveMethod(metadataToken);
                    var fullMethodName = method.DeclaringType?.FullName + "." + method.Name;

                    if (fullMethodName.StartsWith("System.IO.") ||
                        fullMethodName.StartsWith("System.Net.") ||
                        fullMethodName.StartsWith("System.Diagnostics."))
                    {
                        return true;
                    }
                }
                catch
                {
                    return true; // If we can't resolve a method, assume it's suspicious
                }
            }
        }

        return false;
    }
}