using System.Reflection;
using System.Runtime.Loader;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.Events;

namespace PantheonAddonLoader;

public class AddonLoader : MelonMod
{
    public const string ModVersion = "1.0.0";
    
    private static readonly string AddonsFolderPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/PantheonAddons";
    private static AssemblyLoadContext? _assemblyLoadContext;
    
    public static readonly List<Addon> LoadedAddons = new();
    public static readonly WindowPanelEvents WindowPanelEvents = new();
    public static readonly LocalPlayerEvents LocalPlayerEvents = new();
    public static readonly PlayerEvents PlayerEvents = new();
    public static readonly LifecycleEvents LifecycleEvents = new();
    public static readonly ChatEvents ChatEvents = new();

    public static readonly ICustomAssetManager CustomAssetManager = new CustomAssetManager();
    
    public override void OnInitializeMelon()
    {
        if (!Directory.Exists(AddonsFolderPath))
        {
            MelonLogger.Msg($"Creating addons folder {AddonsFolderPath}");
            Directory.CreateDirectory(AddonsFolderPath);
        }
        
        // MelonLoader's OnApplicationQuit doesn't fire unless the game shuts down cleanly
        // e.g., it does not fire if the console window is closed, so instead listen for ProcessExit to save
        // when closed via the console window
        AppDomain.CurrentDomain.ProcessExit += (s, e) => 
        {
            MelonPreferences.Save();
        };
        LoadAddons();
    }

    public override void OnUpdate()
    {
        LifecycleEvents.OnUpdate.Raise();
    }

    public static void ReloadAddons()
    {
        foreach (var addon in LoadedAddons)
        {
            addon.Dispose();
        }

        LoadAddons();
    }
    
    private static void LoadAddons()
    {
        LoadedAddons.Clear();
        
        MelonLogger.Msg($"Loading addons in {AddonsFolderPath}");
        
        _assemblyLoadContext?.Unload();

        _assemblyLoadContext = new AssemblyLoadContext("Addons", true);
        
        foreach (var addonFile in Directory.GetFiles(AddonsFolderPath, "*.dll"))
        {
            // Read using a stream instead of LoadFromFile to prevent locking, and load to a separate assembly context
            // so that we can unload it later, as MelonLoader doesn't like loading an assembly with the same name as
            // an already loaded assembly
            using var reader = File.OpenRead(addonFile);
            var assembly = _assemblyLoadContext.LoadFromStream(reader);
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(Addon)))
                {
                    continue;
                }
                
                var addon = ScriptActivator.ActivateAddon(type);
                if (addon != null)
                {
                    LoadedAddons.Add(addon);
                }
            }
        }
    }
}