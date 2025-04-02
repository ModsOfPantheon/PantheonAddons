using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface IPlayerEvents
{
    AddonEvent<IPlayer> PlayerAdded { get; }
    AddonEvent<IPlayer> PlayerRemoved { get; }
}