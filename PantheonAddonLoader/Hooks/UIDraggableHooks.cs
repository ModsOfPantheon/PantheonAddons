using HarmonyLib;
using Il2Cpp;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.UI;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UIDraggable), nameof(UIDraggable.OnDrag))]
public class RectTransformHooks
{
    private static void Postfix(UIDraggable __instance)
    {
        AddonLoader.WindowPanelEvents.WindowMoved.Raise(new AddonWindow(__instance._windowPanel));
    }
}