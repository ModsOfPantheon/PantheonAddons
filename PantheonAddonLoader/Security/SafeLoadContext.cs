using System.Runtime.Loader;

public class SafeLoadContext : AssemblyLoadContext
{
    public SafeLoadContext() : base(isCollectible: true) { }
}