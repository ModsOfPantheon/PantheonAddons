using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public class LocalPlayerEvents : ILocalPlayerEvents
{
    public AddonEvent<IPlayer> LocalPlayerEntered { get; } = new();
    public AddonEvent<IPlayer> LocalPlayerLeft { get; } = new();
    public AddonEvent<PlayerExperience> ExperienceChanged { get; } = new();
}