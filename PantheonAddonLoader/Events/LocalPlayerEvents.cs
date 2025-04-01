using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public class LocalPlayerEvents : ILocalPlayerEvents
{
    public AddonEvent<IPlayer> OnLocalPlayerEntered { get; } = new();
    public AddonEvent<IPlayer> OnLocalPlayerLeft { get; } = new();
    public AddonEvent<PlayerExperience> OnExperienceChanged { get; } = new();
    public AddonEvent<float> OnOffensiveTargetChanged { get; } = new();
}