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
        var configSection = MelonPreferences.GetCategory(addon.Name) ?? MelonPreferences.CreateCategory(addon.Name);
        var enabled = configSection.GetEntry<bool>(addon.Name)?.Value ?? true;
        if (enabled)
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
                    var floatEntry = configSection.GetEntry<float>(floatConfigurationValue.Name)?.Value ?? configSection.CreateEntry(floatConfigurationValue.Name, floatConfigurationValue.InitialValue).Value;
                    
                    floatConfigurationValue.OnValueChanged(floatEntry);
                    break;
                case IntConfigurationValue intConfigurationValue:
                    var intEntry = configSection.GetEntry<int>(intConfigurationValue.Name)?.Value ?? configSection.CreateEntry(intConfigurationValue.Name, intConfigurationValue.InitialValue).Value;
                    
                    intConfigurationValue.OnValueChanged(intEntry);
                    break;
                case BoolConfigurationValue boolConfigurationValue:
                    var boolEntry = configSection.GetEntry<bool>(boolConfigurationValue.Name)?.Value ?? configSection.CreateEntry(boolConfigurationValue.Name, boolConfigurationValue.InitialValue).Value;
                    
                    boolConfigurationValue.OnValueChanged(boolEntry);
                    break;
                case PicklistConfigurationValue picklistConfigurationValue:
                    var picklistEntry = configSection.GetEntry<int>(picklistConfigurationValue.Name)?.Value ?? configSection.CreateEntry(picklistConfigurationValue.Name, picklistConfigurationValue.InitialIndex).Value;
                    
                    picklistConfigurationValue.OnSelectionChanged(picklistEntry);
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
        instance.Chat = new Chat();

        instance.WindowPanelEvents = AddonLoader.WindowPanelEvents;
        instance.LocalPlayerEvents = AddonLoader.LocalPlayerEvents;
        instance.PlayerEvents = AddonLoader.PlayerEvents;
        instance.LifecycleEvents = AddonLoader.LifecycleEvents;
        instance.ChatEvents = AddonLoader.ChatEvents;
    }
}