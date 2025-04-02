namespace PantheonAddonFramework.Models;

public interface ICurrency
{
    byte Copper { get; }
    byte Silver { get; }
    byte Gold { get; }
    byte Platinum { get; }
    uint Mithril { get; }
    long Total { get; }
}