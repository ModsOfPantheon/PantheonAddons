namespace PantheonAddonFramework.UI;

public interface IAddonPoolBar
{
    IAddonRectTransform Bar { get; }
    IAddonRectTransform Panel { get; }
    IAddonRectTransform TargetNameText { get; }
    IAddonTextComponent AddTextComponent(string initialText);

    void Enable(bool enabled);
    void Destroy();
}