using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface ILocalPlayerEvents
{
    AddonEvent<IPlayer> LocalPlayerEntered { get; }
    AddonEvent<IPlayer> LocalPlayerLeft { get; }
    AddonEvent<PlayerExperience> ExperienceChanged { get; }
}