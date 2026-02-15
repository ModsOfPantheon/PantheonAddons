using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public sealed class ChatEvents : IChatEvents
{
    public AddonEvent<ChatMessage> MessageReceived { get; } = new();
}