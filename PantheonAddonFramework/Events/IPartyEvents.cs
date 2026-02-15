using PantheonAddonFramework.Models;

namespace PantheonAddonFramework.Events;

public interface IPartyEvents
{
    AddonEvent<IPlayer> OtherPlayerJoinedParty { get; }
    AddonEvent<IPlayer> OtherPlayerLeftParty { get; }
    AddonEvent<IEnumerable<IPlayer>> LocalPlayerJoinedParty { get; }
    AddonEvent LocalPlayerLeftParty { get; }
    AddonEvent PartyDisbanded { get; }
}