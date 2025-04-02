using PantheonAddonFramework;
using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public class PlayerEvents : IPlayerEvents
{
    public AddonEvent<IPlayer> PlayerAdded { get; } = new();
    public AddonEvent<IPlayer> PlayerRemoved { get; } = new();
}