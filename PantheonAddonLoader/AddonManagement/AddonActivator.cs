using System.Linq.Expressions;
using System.Reflection;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonLoader.AddonComponents;

namespace PantheonAddonLoader.AddonManagement;

internal static class ScriptActivator
{
    internal static Addon? ActivateAddon(Type addonType)
    {
        var expression = Expression.New(addonType);
        var lambda = Expression.Lambda<Func<object>>(expression);
        var compiled = lambda.Compile();
        
        if (compiled() is not Addon instance)
        {
            return null;
        }

        var scriptAttribute = addonType.GetCustomAttribute<AddonMetadataAttribute>(true);

        if (scriptAttribute == null)
        {
            MelonLogger.Error($"Failed to load {addonType.Name}, make sure it has a {nameof(scriptAttribute)}");
            return null;
        }
        
        RegisterDependencies(instance);

        instance.Name = scriptAttribute.Name;
        instance.Author = scriptAttribute.Author;
        instance.Description = scriptAttribute.Description;
        instance.Enabled = true;

        instance.OnCreate();
        
        MelonLogger.Msg($"Loaded [{scriptAttribute.Author}] {scriptAttribute.Name}");
        
        instance.Enable();
        
        return instance;
    }

    private static void RegisterDependencies(Addon instance)
    {
        instance.Logger = new AddonLogger();
        instance.Keyboard = new Keyboard();
        instance.Macros = new Macros();
        instance.CustomUI = new CustomUI();
        
        instance.WindowPanelEvents = AddonLoader.WindowPanelEvents;
        instance.LocalPlayerEvents = AddonLoader.LocalPlayerEvents;
        instance.PlayerEvents = AddonLoader.PlayerEvents;
        instance.LifecycleEvents = AddonLoader.LifecycleEvents;
    }
}