namespace PantheonAddonFramework.UI;

public interface IXpBarWindow : IAddonWindow
{
    void ShowTicks(bool show);
    void SetBackgroundColour(float red, float green, float blue, float alpha);
    void SetSliderColour(float red, float green, float blue, float alpha);
}