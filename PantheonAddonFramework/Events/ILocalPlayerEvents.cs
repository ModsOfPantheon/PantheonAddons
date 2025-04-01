using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface ILocalPlayerEvents
{
    AddonEvent<IPlayer> OnLocalPlayerEntered { get; }
    AddonEvent<IPlayer> OnLocalPlayerLeft { get; }
    AddonEvent<PlayerExperience> OnExperienceChanged { get; }
    AddonEvent<float> OnOffensiveTargetChanged { get; }
}