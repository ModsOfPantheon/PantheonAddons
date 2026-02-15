using PantheonAddonFramework.Events;

namespace PantheonAddonLoader.Events;

public sealed class LifecycleEvents : ILifecycleEvents
{
    public AddonEvent OnUpdate { get; } = new();
}