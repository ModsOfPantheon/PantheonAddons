namespace PantheonAddonFramework.UI;

public interface IAddonTextComponent
{
    string GetText();
    void SetText(string text);
    void SetSize(float width, float height);
    void SetFontSize(float fontSize);
    public float GetFontSize();
    void SetFontColor(byte red, byte green, byte blue, byte alpha);
    void SetFontColor(float red, float green, float blue, float alpha);
    void Enable(bool enabled);
    void Destroy();
}