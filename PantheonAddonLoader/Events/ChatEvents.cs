using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public class ChatEvents : IChatEvents
{
    public AddonEvent<ChatMessage> OnMessageReceived { get; } = new();
}