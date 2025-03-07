namespace PantheonAddonLoader.Security;

public interface IAddonScanning
{
    bool ShouldLoad(string addonFile);
}