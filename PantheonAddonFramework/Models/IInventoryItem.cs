namespace PantheonAddonFramework.Models;

public interface IInventoryItem
{
    Guid Id { get; }
    
    string Name { get; }
}