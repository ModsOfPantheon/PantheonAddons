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
    /// <summary>
    /// Set TextComponent font to provided font name, for example to use Impact.ttf, call method with "Impact"
    /// </summary>
    /// <param name="font">Filename for font without the .ttf extension</param>
    void SetFont(string font);
    void Enable(bool enabled);
    void Destroy();
}