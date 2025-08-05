using HarmonyLib;
using Il2Cpp;
using PantheonAddonFramework;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.UI;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UIWindowPanel), nameof(UIWindowPanel.Start))]
public class UIPanelHooks
{
    private static void Postfix(UIWindowPanel __instance)
    {
        if (__instance.name == "Panel_XpBar")
        {
            AddonLoader.WindowPanelEvents.ExperienceBarReady.Raise(new XpBarWindow(__instance));
        }
        if (__instance.name == "Panel_OffensiveTarget")
        {
            var healthBarPool = __instance.GetComponentInChildren<UIPoolBar>(true);
            AddonLoader.WindowPanelEvents.OffensiveTargetReady.Raise(new AddonPoolBar(healthBarPool));
        }
        if (__instance.name == "Panel_DefensiveTarget")
        {
            var healthBarPool = __instance.GetComponentInChildren<UIPoolBar>(true);
            AddonLoader.WindowPanelEvents.DefensiveTargetReady.Raise(new AddonPoolBar(healthBarPool));
        }
    }
}

[HarmonyPatch(typeof(UIPoolBar), nameof(UIPoolBar.HandlePoolChanged))]
public class UIPoolBarHandlePoolChangedHook
{
    private static void Postfix(UIPoolBar __instance, float current, float max, float delta, PoolChangeType changeType)
    {
        var pppName = __instance.transform.parent.parent.parent.name;

        if (pppName == "Panel_OffensiveTarget")
        {
            var percent = current / max * 100;
            AddonLoader.WindowPanelEvents.OffTargetPoolbarChange.Raise(percent);
        }
        if (pppName == "Panel_DefensiveTarget")
        {
            var percent = current / max * 100;
            AddonLoader.WindowPanelEvents.DefTargetPoolbarChange.Raise(percent);
        }
    }
}