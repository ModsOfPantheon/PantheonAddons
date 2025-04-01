using Il2Cpp;
using PantheonAddonFramework.Models;

namespace PantheonAddonLoader.Models;

public class PlayerCurrency : ICurrency
{
    private readonly EntityCurrency.Logic _currency;

    public PlayerCurrency(EntityCurrency.Logic currency)
    {
        _currency = currency;
    }

    public byte Copper => _currency.Current.Copper;
    public byte Silver => _currency.Current.Silver;
    public byte Gold => _currency.Current.Gold;
    public byte Platinum => _currency.Current.Platinum;
    public uint Mithril => _currency.Current.Mithril;
    public long Total => _currency.Current.Total;
}