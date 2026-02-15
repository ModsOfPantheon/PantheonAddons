using PantheonAddonFramework.Events;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Events;

public sealed class PartyEvents : IPartyEvents
{
    public AddonEvent<IPlayer> OtherPlayerJoinedParty { get; }
    public AddonEvent<IPlayer> OtherPlayerLeftParty { get; }
    public AddonEvent<IEnumerable<IPlayer>> LocalPlayerJoinedParty { get; }
    public AddonEvent LocalPlayerLeftParty { get; }
    public AddonEvent PartyDisbanded { get; }
}