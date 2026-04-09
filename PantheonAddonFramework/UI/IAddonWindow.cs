namespace PantheonAddonFramework.UI;

public interface IAddonWindow : IAddonUIElement
{
    float Height { get; }
    float Width { get; }
    float X { get; }
    float Y { get; }
    void SetHeight(float newHeight);
    void SetWidth(float newWidth);
    void SetPosition(float newX, float newY);

    IAddonWindow AddResizeHandle(int maxWidth, int maxHeight, int minWidth, int minHeight);
    
    void Enable(bool enabled);
    void Destroy();
    ILayoutObject AddVerticalLayout();
}