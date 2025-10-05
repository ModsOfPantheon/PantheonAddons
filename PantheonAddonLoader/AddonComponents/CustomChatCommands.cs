using PantheonAddonFramework.AddonComponents;

namespace PantheonAddonLoader.AddonComponents;

public class CustomChatCommands : ICustomChatCommands
{
    private readonly Dictionary<string, Action<string[]>> _commands = new();
    
    public void Add(string command, Action<string[]> callback)
    {
        _commands.TryAdd(command, callback);
    }

    public void Remove(string command)
    {
        if (_commands.ContainsKey(command))
        {
            _commands.Remove(command);
        }
    }

    internal bool Handle(string message)
    {
        var split = message.Split(' ');
        
        var command = split[0];
        if (!_commands.TryGetValue(command, out var callback))
        {
            return false;
        }

        callback(split[1..]);

        return true;
    }
}