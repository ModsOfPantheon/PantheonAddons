namespace PantheonAddonFramework.Models;

public interface IMacro
{
    string Name { get; }
    void Activate(bool allowCancelling = true);
}
