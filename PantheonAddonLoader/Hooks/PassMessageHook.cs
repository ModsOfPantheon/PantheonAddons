using HarmonyLib;
using Il2Cpp;
using Il2CppPantheonPersist;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Hooks;

[HarmonyPatch(typeof(UIChatWindows), nameof(UIChatWindows.PassMessage), typeof(string), typeof(string), typeof(ChatChannelType))]
public class PassMessageHook
{
    private static void Postfix(UIChatWindows __instance, string name, string message, ChatChannelType channel)
    {
        AddonLoader.ChatEvents.MessageReceived.Raise(new ChatMessage(name, message, channel.ToString()));
    }
}