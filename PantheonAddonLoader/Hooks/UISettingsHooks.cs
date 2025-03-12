using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using MelonLoader;
using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;
using PantheonAddonLoader.AddonComponents;
using PantheonAddonLoader.AddonManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UISettings), nameof(UISettings.Awake))]
public class UISettingsHooks
{
    private static List<GameObject> CustomUIComponents = new();
    
    private static void Postfix(UISettings __instance)
    {
        // TODO: Move this out of the hook in to another evented class
        if (__instance.transform.childCount < 11)
        {
            // This ran at character select
            return;
        }

        var modTabPage = CreateAddonPage(__instance);

        CreateReloadButton(__instance, modTabPage);
        
        CreateAddonConfigurationElements(__instance, modTabPage);
    }

    private static Transform CreateAddonPage(UISettings instance)
    {
        var tabButtons = instance.transform.GetChild(3);
        var otherButton = tabButtons.GetChild(5);
        var otherTabPage = instance.transform.GetChild(9);

        var addonTabButton = Object.Instantiate(otherButton, otherButton.position, otherButton.rotation, tabButtons);
        addonTabButton.name = "Addons";
        var tabPageButton = addonTabButton.GetComponent<UITabPageButton>();
        tabPageButton.Text.text = "Addons";
        
        var modTabPage = Object.Instantiate(otherTabPage, otherTabPage.position, otherTabPage.rotation, instance.transform);
        modTabPage.name = "TabPage_Addons";
        
        var newTabPageLayout = modTabPage.GetChild(0);
        
        for (var i = 0; i < newTabPageLayout.childCount; i++)
        {
            Object.Destroy(newTabPageLayout.GetChild(i).gameObject);
        }
        
        var uiTabPage = modTabPage.GetComponent<UITabPage>();
        uiTabPage.TabButton = tabPageButton;
        tabPageButton.TabPage = uiTabPage;
        
        var parentRect = instance.GetComponent<RectTransform>();
        var parentSize = parentRect.sizeDelta;
        parentRect.sizeDelta =
            new Vector2(parentSize.x + addonTabButton.GetComponent<RectTransform>().sizeDelta.x, parentSize.y);
        return modTabPage;
    }

    private static void CreateReloadButton(UISettings instance, Transform modTabPage)
    {
        var tabInput = instance.transform.GetChild(7);
        var inputLayout = tabInput.GetChild(0);
        var keybindButton = inputLayout.GetChild(2);
        
        var keybindCopy = Object.Instantiate(keybindButton, keybindButton.position, keybindButton.rotation,
            modTabPage.GetChild(0));
        
        var keybindCopyText = keybindCopy.GetChild(0).GetComponent<TextMeshProUGUI>();
        keybindCopyText.text = "Addons";

        var keybindCopyButtonObject = keybindCopy.GetChild(1);
        var keybindCopyButtonText = keybindCopyButtonObject.GetChild(0).GetComponent<TextMeshProUGUI>();
        keybindCopyButtonText.text = "Reload";
        var keybindCopyButton = keybindCopyButtonObject.GetComponent<Button>();
        keybindCopyButton.onClick = new Button.ButtonClickedEvent();
        keybindCopyButton.onClick.RemoveAllListeners();
        keybindCopyButton.onClick.AddCall(new InvokableCall(new Action(() =>
        {
            AddonLoader.ReloadAddons();
            
            foreach (var t in CustomUIComponents)
            {
                Object.Destroy(t.gameObject);
            }
            
            CustomUIComponents.Clear();

            CreateAddonConfigurationElements(instance, modTabPage);
        })));
    }

    private static void CreateAddonConfigurationElements(UISettings instance, Transform modTabPage)
    {
        var tabGeneral = instance.transform.GetChild(4);
        var generalLayout = tabGeneral.GetChild(0);
        var spacer = generalLayout.GetChild(0);
        var sliderToCopy = generalLayout.GetChild(11);
        var picklistToCopy = generalLayout.GetChild(4);
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
            
            var spacerCopy = Object.Instantiate(spacer, spacer.transform.position, spacer.transform.rotation, modTabPage.GetChild(0));
            CustomUIComponents.Add(spacerCopy.gameObject);
        }
    }

    private static void SetupCustomPicklist(Addon addon, PicklistConfigurationValue configuration, Transform parent, Transform picklistToCopy)
    {
        // If a blank string array is passed in, this breaks the loading of dropdown values.
        // Gameobject is never added to the CustomUIComponents list and reload addons is broken. 
        if (configuration.Values.Count() < 1)
            return;
        
        var copy = Object.Instantiate(picklistToCopy, picklistToCopy.position, picklistToCopy.rotation, parent);
        copy.name = $"Picklist_{addon.Name}_{configuration.Name}";
        
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

        var category = MelonPreferences.GetCategory(addon.Name);
        var entry = category.GetEntry<int>(configuration.Name)
                    ?? category.CreateEntry(configuration.Name, configuration.InitialIndex);
        
        dropDown.value = entry.Value;
        
        dropDown.onValueChanged.RemoveAllListeners();
        dropDown.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            configuration.OnSelectionChanged(dropDown.value);
            entry.Value = dropDown.value;
        })));
        
        CustomUIComponents.Add(copy.gameObject);
    }

    private static void SetupCustomToggle(Addon addon, BoolConfigurationValue configuration, Transform parent, Transform buttonToCopy)
    {
        var copy = Object.Instantiate(buttonToCopy, buttonToCopy.position, buttonToCopy.rotation, parent);
        copy.name = $"Toggle_{addon.Name}_{configuration.Name}";

        var tooltip = copy.GetOrAddComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
        
        copy.GetChild(0).GetComponent<TextMeshProUGUI>().text = configuration.Name;
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigBool>());
        
        var toggleComp = copy.GetComponent<Toggle>();
        
        var category = MelonPreferences.GetCategory(addon.Name);
        var entry = category.GetEntry<bool>(configuration.Name) ?? category.CreateEntry(configuration.Name, configuration.InitialValue);
        
        toggleComp.isOn = entry.Value;
        toggleComp.onValueChanged.RemoveAllListeners();
        toggleComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            configuration.OnValueChanged(toggleComp.isOn);
            entry.Value = toggleComp.isOn;
        })));
        
        CustomUIComponents.Add(copy.gameObject);
    }

    private static void SetupCustomSlider(Addon addon, FloatConfigurationValue configuration, Transform parent, Transform sliderToCopy)
    {
        var copy = Object.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configuration.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configuration.Name} - {configuration.InitialValue:F1}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configuration.MinValue;
        sliderComp.maxValue = configuration.MaxValue;
        sliderComp.wholeNumbers = false;
        
        var category = MelonPreferences.GetCategory(addon.Name);
        var entry = category.GetEntry<float>(configuration.Name) ?? category.CreateEntry(configuration.Name, configuration.InitialValue);
        sliderComp.value = entry.Value;
        textComp.text = $"{configuration.Name} - {sliderComp.value:F1}";
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configuration.StepAmount);
            textComp.text = $"{configuration.Name} - {sliderComp.value:F1}";
            
            configuration.OnValueChanged(MathF.Round(sliderComp.value, 1));
            
            entry.Value = MathF.Round(sliderComp.value, 1);
        })));
        
        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
        
        CustomUIComponents.Add(copy.gameObject);
    }
    
    // TODO: Clean up duplicate code between this method and one above
    private static void SetupCustomSlider(Addon addon, IntConfigurationValue configuration, Transform parent, Transform sliderToCopy)
    {
        var copy = Object.Instantiate(sliderToCopy, sliderToCopy.position, Quaternion.identity, parent);
        copy.name = $"Slider_{addon.Name}_{configuration.Name}";
        
        Object.Destroy(copy.GetComponent<UISettings_ConfigSlider>());
        
        var textComp = copy.GetChild(0).GetComponent<TextMeshProUGUI>();
        textComp.text = $"{configuration.Name} - {configuration.InitialValue}";
        
        var sliderObj = copy.GetChild(1);
        var sliderComp = sliderObj.GetComponent<Slider>();
        sliderComp.minValue = configuration.MinValue;
        sliderComp.maxValue = configuration.MaxValue;
        sliderComp.wholeNumbers = true;
        
        var category = MelonPreferences.GetCategory(addon.Name);
        var configEntry = category.GetEntry<int>(configuration.Name) ?? category.CreateEntry(configuration.Name, configuration.InitialValue);
        
        sliderComp.value = configEntry.Value;
        textComp.text = $"{configuration.Name} - {sliderComp.value}";
        
        sliderComp.onValueChanged.RemoveAllListeners();
        sliderComp.onValueChanged.AddCall(new InvokableCall(new Action(() =>
        {
            sliderComp.value = GetNearestMultiple(sliderComp.value, configuration.StepAmount);
            textComp.text = $"{configuration.Name} - {sliderComp.value}";
            
            configuration.OnValueChanged((int)sliderComp.value);
            configEntry.Value = (int)sliderComp.value;
        })));

        var handleObject = sliderObj.GetChild(2).GetChild(0);
        var tooltip = handleObject.GetComponent<UITooltip>();
        tooltip.TooltipHeadingText = configuration.Name;
        tooltip.TooltipText = configuration.Description;
        
        CustomUIComponents.Add(copy.gameObject);
    }
    
    private static float GetNearestMultiple(float number, float multiple)
    {
        return (float)(Math.Round(number / multiple) * multiple);
    }
}