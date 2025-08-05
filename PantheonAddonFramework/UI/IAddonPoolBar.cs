namespace PantheonAddonFramework.UI;

public interface IAddonPoolBar
{
    void SetupWindow();
    IAddonTextComponent AddTextComponent(string initialText);

    void Enable(bool enabled);
    void Destroy();
}