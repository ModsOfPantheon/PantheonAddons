namespace PantheonAddonFramework.Models;

public interface IPlayer
{
    IEntityStats Stats { get; }
    
    ICurrency InventoryCurrency { get; }
    
    ICurrency BankCurrency { get; }

    long CharacterId { get; }
    
    string Name { get; }

    int Level { get; }

    bool IsMale { get; }

    string Race { get; }

    string Class { get; }

    PlayerExperience? GetExperience();

    bool IsLocalPlayer { get; }
}