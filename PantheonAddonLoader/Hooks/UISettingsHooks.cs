using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonLoader.AddonManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UISettings), nameof(UISettings.Awake))]
public class UISettingsHooks
{
    private static void Postfix(UISettings __instance)
    {
        // TODO: Move this out of the hook in to another evented class
        if (__instance.transform.childCount < 11)
        {
            return;
        }

        var tabButtons = __instance.transform.GetChild(3);

        var otherButton = tabButtons.GetChild(5);

        var tabGeneral = __instance.transform.GetChild(4);
        var generalLayout = tabGeneral.GetChild(0);
        var spacer = generalLayout.GetChild(0);
        var sliderToCopy = generalLayout.GetChild(11);
        var picklistToCopy = generalLayout.GetChild(4);
        
        var tabOther = __instance.transform.GetChild(9);
        
        var newButton = Object.Instantiate(otherButton, otherButton.position, otherButton.rotation, tabButtons);
        newButton.name = "Addons";
        var tabPageButton = newButton.GetComponent<UITabPageButton>();
        tabPageButton.Text.text = "Addons";
        
        var tabPageOther = __instance.transform.GetChild(9);
        var modTabPage = Object.Instantiate(tabPageOther, tabPageOther.position, tabPageOther.rotation,
            __instance.transform);
        modTabPage.name = "TabPage_Addons";
        
        var newTabPageLayout = modTabPage.GetChild(0);
        
        for (var i = 0; i < newTabPageLayout.childCount; i++)
        {
            Object.Destroy(newTabPageLayout.GetChild(i).gameObject);
        }
        
        var uiTabPage = modTabPage.GetComponent<UITabPage>();
        uiTabPage.TabButton = tabPageButton;
        tabPageButton.TabPage = uiTabPage;
        
        var parentRect = __instance.GetComponent<RectTransform>();
        var parentSize = parentRect.sizeDelta;
        parentRect.sizeDelta =
            new Vector2(parentSize.x + newButton.GetComponent<RectTransform>().sizeDelta.x, parentSize.y);

        var buttonToCopy = generalLayout.GetChild(1);
        
        foreach (var addon in AddonLoader.LoadedAddons)
        {
            var configurationValue = new BoolConfigurationValue(addon.Name, addon.Description, true, b =>
            {
                if (b)
                {
                    addon.Enable();
                }
                else
                {
                    addon.Disable();
                }
            });
            
            SetupCustomToggle(addon, configurationValue, modTabPage.GetChild(0), buttonToCopy);

            foreach (var configuration in addon.GetConfiguration())
            {
                switch (configuration)
                {
                    case FloatConfigurationValue floatConfigurationValue:
                        SetupCustomSlider(addon, floatConfigurationValue, modTabPage.GetChild(0), sliderToCopy);
                        break;
                    case IntConfigurationValue intConfigurationValue:
                        SetupCustomSlider(addon, intConfigurationValue, modTabPage.GetChild(0), sliderToCopy);
                        break;
                    case BoolConfigurationValue boolConfigurationValue:
                        SetupCustomToggle(addon, boolConfigurationValue, modTabPage.GetChild(0), buttonToCopy);
                        break;
                    case PicklistConfigurationValue picklistConfigurationValue:
                        SetupCustomPicklist(addon, picklistConfigurationValue, modTabPage.GetChild(0), picklistToCopy);
                        break;
                }
            }
            
            GameObject.Instantiate(spacer, spacer.transform.position, spacer.transform.rotation, modTabPage.GetChild(0));
        }
    }

    private static void SetupCustomPicklist(Addon addon, PicklistConfigurationValue configuration, Transform parent, Transform picklistToCopy)
    {
        var copy = GameObject.Instantiate(picklistToCopy, picklistToCopy.position, picklistToCopy.rotation, parent);
        copy.name = $"Toggle_{addon.Name}_{configuration.Name}";
        
        var tooltip = copy.GetOrAddComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigDropdown>());

        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = configuration.Name;

        var dropDown = copy.GetChild(1).GetComponent<TMP_Dropdown>();
        dropDown.ClearOptions();
        foreach (var option in configuration.Values)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData
            {
                text = option
            });
        }

        var category = MelonPreferences.CreateCategory(addon.Name);
        var entry = category.CreateEntry(configuration.Name, configuration.InitialIndex);
        
        dropDown.value = entry.Value;
        
        dropDown.onValueChanged.RemoveAllListeners();
        dropDown.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            configuration.OnSelectionChanged(dropDown.value);
            entry.Value = dropDown.value;
        })));
        
        dropDown.onValueChanged.Invoke(dropDown.value);
    }

    private static void SetupCustomToggle(Addon addon, BoolConfigurationValue configuration, Transform parent, Transform buttonToCopy)
    {
        var copy = GameObject.Instantiate(buttonToCopy, buttonToCopy.position, buttonToCopy.rotation, parent);
        copy.name = $"Toggle_{addon.Name}_{configuration.Name}";

        var tooltip = copy.GetOrAddComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
        
        copy.GetChild(0).GetComponent<TextMeshProUGUI>().text = configuration.Name;
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigBool>());
        
        var toggleComp = copy.GetComponent<Toggle>();
        
        var category = MelonPreferences.CreateCategory(addon.Name);
        var entry = category.CreateEntry(configuration.Name, configuration.InitialValue);
        
        toggleComp.isOn = entry.Value;
        toggleComp.onValueChanged.RemoveAllListeners();
        toggleComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            configuration.OnValueChanged(toggleComp.isOn);
            entry.Value = toggleComp.isOn;
        })));

        toggleComp.onValueChanged.Invoke(toggleComp.isOn);
    }

    private static void SetupCustomSlider(Addon addon, FloatConfigurationValue configuration, Transform parent, Transform sliderToCopy)
    {
        var copy = GameObject.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configuration.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configuration.Name} - {configuration.InitialValue:F1}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configuration.MinValue;
        sliderComp.maxValue = configuration.MaxValue;
        sliderComp.wholeNumbers = false;
        
        var category = MelonPreferences.CreateCategory(addon.Name);
        var entry = category.CreateEntry(configuration.Name, configuration.InitialValue);
        sliderComp.value = entry.Value;
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configuration.StepAmount);
            textComp.text = $"{configuration.Name} - {sliderComp.value:F1}";
            
            configuration.OnValueChanged(MathF.Round(sliderComp.value, 1));
            
            entry.Value = MathF.Round(sliderComp.value, 1);
        })));
        
        sliderComp.onValueChanged.Invoke(MathF.Round(sliderComp.value, 1));
        
        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
    }
    
    // TODO: Clean up duplicate code between this method and one above
    private static void SetupCustomSlider(Addon addon, IntConfigurationValue configuration, Transform parent, Transform sliderToCopy)
    {
        var copy = GameObject.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configuration.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configuration.Name} - {configuration.InitialValue}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configuration.MinValue;
        sliderComp.maxValue = configuration.MaxValue;
        sliderComp.wholeNumbers = true;
        
        var category = MelonPreferences.CreateCategory(addon.Name);
        var configEntry = category.CreateEntry(configuration.Name, configuration.InitialValue);
        
        sliderComp.value = configEntry.Value;
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configuration.StepAmount);
            textComp.text = $"{configuration.Name} - {sliderComp.value}";
            
            configuration.OnValueChanged((int)sliderComp.value);
            configEntry.Value = (int)sliderComp.value;
        })));

        sliderComp.onValueChanged.Invoke(configEntry.Value);
        
        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
    }
    
    private static float GetNearestMultiple(float number, float multiple)
    {
        return (float)(Math.Round(number / multiple) * multiple);
    }
}