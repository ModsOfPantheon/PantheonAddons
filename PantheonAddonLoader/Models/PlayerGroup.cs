using System.Text.RegularExpressions;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

public class PlayerGroup : IPlayerGroup
{
    private readonly Il2Cpp.Group.Logic _group;

    public PlayerGroup(Il2Cpp.Group.Logic group)
    {
        _group = group;
    }


    public IReadOnlyCollection<GroupMember> GetGroupMembers()
    {
        var players = new List<GroupMember>();
        foreach (var player in _group.Current.members)
        {
            players.Add(new GroupMember(player.Name, player.Class.ToString(), player.Gender.ToString(), player.CharacterId, player.EntityNetworkId.Value, player.IsLeader));
        }

        return players;
    }

    public bool IsGroupLeader => _group.IsGroupLeader;
    public bool IsInGroup => _group.IsInGroup;
}