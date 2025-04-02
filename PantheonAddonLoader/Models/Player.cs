using Il2Cpp;
using Il2CppPantheonPersist;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

public class Player : IPlayer
{
    private readonly EntityPlayerGameObject _entityPlayerGameObject;
    public IEntityStats Stats { get; }
    public ICurrency InventoryCurrency { get; }
    public ICurrency BankCurrency { get; }

    public Player(EntityPlayerGameObject entityPlayerGameObject)
    {
        _entityPlayerGameObject = entityPlayerGameObject;
        Stats = new EntityStats(_entityPlayerGameObject.Pools);
        InventoryCurrency = new PlayerCurrency(_entityPlayerGameObject.Currency);
        BankCurrency = new BankCurrency(_entityPlayerGameObject.BankCurrency);
    }

    public long CharacterId => _entityPlayerGameObject.info.CharacterId;
    public string Name => _entityPlayerGameObject.info.DisplayName;
    public int Level => _entityPlayerGameObject.Experience.Level;
    public bool IsMale => _entityPlayerGameObject.info.Gender == Gender.Male;
    public string Race => _entityPlayerGameObject.info.Race.ToString();
    public string Class => _entityPlayerGameObject.info.Class.ToString();
    
    public PlayerExperience GetExperience()
    {
        var experience = _entityPlayerGameObject.Experience;
        return new PlayerExperience(experience.CalculateCurrentExperienceIntoLevel(), experience.CalculateExperienceRequiredToNextLevel(), experience.CalculatePercentThroughCurrentLevel());
    }

    public bool IsLocalPlayer => _entityPlayerGameObject.NetworkId.Value == EntityPlayerGameObject.LocalPlayerId.Value;
}