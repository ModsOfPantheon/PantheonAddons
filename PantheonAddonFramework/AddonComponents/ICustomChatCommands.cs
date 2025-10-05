namespace PantheonAddonFramework.AddonComponents;

public interface ICustomChatCommands
{
    void Add(string command, Action<string[]> callback);
    void Remove(string command);
}