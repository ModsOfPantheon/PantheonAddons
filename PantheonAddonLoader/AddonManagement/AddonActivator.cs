using System.Linq.Expressions;
using System.Reflection;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
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

        instance.OnCreate();

        SetupConfiguration(instance);

        MelonLogger.Msg($"Loaded [{scriptAttribute.Author}] {scriptAttribute.Name}");

        return instance;
    }

    private static void SetupConfiguration(Addon addon)
    {
        var configSection = MelonPreferences.CreateCategory(addon.Name);
        var enabled = configSection.CreateEntry(addon.Name, true);
        if (enabled.Value)
        {
            addon.Enable();
        }
        else
        {
            addon.Disable();
        }

        foreach (var configuration in addon.GetConfiguration())
        {
            switch (configuration)
            {
                case FloatConfigurationValue floatConfigurationValue:
                    var floatEntry = configSection.CreateEntry(floatConfigurationValue.Name,
                        floatConfigurationValue.InitialValue);
                    floatConfigurationValue.OnValueChanged(floatEntry.Value);
                    break;
                case IntConfigurationValue intConfigurationValue:
                    var intEntry =
                        configSection.CreateEntry(intConfigurationValue.Name, intConfigurationValue.InitialValue);
                    intConfigurationValue.OnValueChanged(intEntry.Value);
                    break;
                case BoolConfigurationValue boolConfigurationValue:
                    var boolEntry = configSection.CreateEntry(boolConfigurationValue.Name,
                        boolConfigurationValue.InitialValue);
                    boolConfigurationValue.OnValueChanged(boolEntry.Value);
                    break;
                case PicklistConfigurationValue picklistConfigurationValue:
                    var picklistEntry = configSection.CreateEntry(picklistConfigurationValue.Name,
                        picklistConfigurationValue.InitialIndex);
                    picklistConfigurationValue.OnSelectionChanged(picklistEntry.Value);
                    break;
            }
        }
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