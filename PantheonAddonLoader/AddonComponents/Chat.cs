using Il2Cpp;
using Il2CppPantheonPersist;
using PantheonAddonFramework.AddonComponents;

namespace PantheonAddonLoader.AddonComponents;

public class Chat : IChat
{
    public void AddInfoMessage(string message)
    {
        UIChatWindows.Instance.PassMessage(message, ChatChannelType.Info);
    }
}