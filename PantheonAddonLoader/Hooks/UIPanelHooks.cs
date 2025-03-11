using HarmonyLib;
using Il2Cpp;
using PantheonAddonFramework;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.UI;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UIWindowPanel), nameof(UIWindowPanel.Start))]
public class UIPanelHooks
{
    private static void Postfix(UIWindowPanel __instance)
    {
        if (__instance.name == "Panel_XpBar")
        {
            AddonLoader.WindowPanelEvents.OnExperienceBarReady.Raise(new XpBarWindow(__instance));
        }
    }
}