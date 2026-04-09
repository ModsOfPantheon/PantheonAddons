namespace PantheonAddonFramework.UI;

public interface IAddonUIElement
{
    IAddonImageComponent AddImageComponent(string objectName);
    IAddonTextComponent AddTextComponent(string initialText);
}
