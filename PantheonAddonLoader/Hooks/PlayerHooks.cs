using HarmonyLib;
using Il2Cpp;
using Il2CppPantheonPersist;
using PantheonAddonFramework.Models;
using PantheonAddonLoader.AddonManagement;
using PantheonAddonLoader.Models;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(EntityPlayerGameObject), nameof(EntityPlayerGameObject.NetworkStart))]
public class PlayerNetworkStart
{
    private static void Postfix(EntityPlayerGameObject __instance)
    {
        // Fired in character select
        if (__instance.NetworkId.Value == 1)
        {
            return;
        }

        var player = new Player(__instance);
        
        if (player.IsLocalPlayer)
        {
            Globals.LocalPlayer = __instance;
            AddonLoader.LocalPlayerEvents.LocalPlayerEntered.Raise(player);
        }

        AddonLoader.PlayerEvents.PlayerAdded.Raise(player);
    }
}

[HarmonyPatch(typeof(Experience.Logic), nameof(Experience.Logic.SetExperience))]
public class ExperienceSetHook
{
    private static void Postfix(Experience.Logic __instance)
    {
        if (Globals.LocalPlayer?.Experience == __instance)
        {
            AddonLoader.LocalPlayerEvents.ExperienceChanged.Raise(new PlayerExperience(__instance.CalculateCurrentExperienceIntoLevel(), __instance.CalculateExperienceRequiredToNextLevel(), __instance.CalculatePercentThroughCurrentLevel()));
        }
    }
}

[HarmonyPatch(typeof(Targets.Logic), nameof(Targets.Logic.SetOffensive))]
public class TargetSetOffensiveHook
{
    private static void Postfix(Targets.Logic __instance)
    {
        if (Globals.LocalPlayer?.Targets == __instance)
        {
            float percent = 0;
            if (__instance.Offensive != null)
            {
                var current = __instance.Offensive.Pools.GetCurrent(PoolType.Health);
                var max = __instance.Offensive.Pools.GetMax(PoolType.Health);
                percent = current / max * 100;
            }              
 
            AddonLoader.LocalPlayerEvents.OffensiveTargetChanged.Raise(percent);
        }
    }
}

[HarmonyPatch(typeof(Targets.Logic), nameof(Targets.Logic.SetDefensive))]
public class TargetSetDefensiveHook
{
    private static void Postfix(Targets.Logic __instance)
    {
        if (Globals.LocalPlayer?.Targets == __instance)
        {
            float percent = 0;
            if (__instance.Defensive != null)
            {
                var current = __instance.Defensive.Pools.GetCurrent(PoolType.Health);
                var max = __instance.Defensive.Pools.GetMax(PoolType.Health);
                percent = current / max * 100;
            }

            AddonLoader.LocalPlayerEvents.DefensiveTargetChanged.Raise(percent);
        }
    }
}