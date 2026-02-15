using PantheonAddonLoader.Models;

namespace PantheonAddonFramework.Models;

public interface IPlayerGroup
{
    IReadOnlyCollection<GroupMember> GetGroupMembers();
    bool IsGroupLeader { get; }
    bool IsInGroup { get; }
}