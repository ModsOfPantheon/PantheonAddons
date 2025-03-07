using PantheonAddonFramework;
using PantheonAddonFramework.Configuration;

namespace PantheonAddons;

[AddonMetadata("Sus", "Test", "Tests security measures")]
public sealed class SusAddon : Addon
{
    static SusAddon()
    {
        
    }
    
    public override void OnCreate()
    {
    }

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }

    public override IEnumerable<IConfigurationValue> GetConfiguration()
    {
        return Array.Empty<IConfigurationValue>();
    }

    public override void Dispose()
    {
    }
}