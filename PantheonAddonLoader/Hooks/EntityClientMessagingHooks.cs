using HarmonyLib;
using Il2Cpp;
using Il2CppPantheonPersist;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(EntityClientMessaging.Logic), nameof(EntityClientMessaging.Logic.SendChatMessage), typeof(string), typeof(ChatChannelType))]
public class SendChatMessageHook
{
    private static bool Prefix(EntityClientMessaging.Logic __instance, string message, ChatChannelType channel)
    {
        var response = AddonLoader.CustomChatCommands.Handle(message);
        
        return !response;
    }
}

[HarmonyPatch(typeof(EntityClientMessaging.Logic), nameof(EntityClientMessaging.Logic.RequestWhisper))]
public class RequestWhisperHook
{
    private static bool Prefix(EntityClientMessaging.Logic __instance, string targetPlayerName, string message)
    {
        var response = AddonLoader.CustomChatCommands.Handle(message);
        
        return !response;
    }
}