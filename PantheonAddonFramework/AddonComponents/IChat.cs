namespace PantheonAddonFramework.AddonComponents;

public interface IChat
{
    /// <summary>
    /// Adds a message to the chat that has the info chat type. This is visible only to the client.
    /// If the user has filtered info messages, the message will not be displayed.
    /// </summary>
    /// <param name="message"></param>
    void AddInfoMessage(string message);
}