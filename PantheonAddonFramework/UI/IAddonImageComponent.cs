namespace PantheonAddonFramework.UI;

public interface IAddonImageComponent
{
    void SetSprite(string filePath);

    void SetColour(byte r, byte g, byte b, byte a);

    void SetColour(float r, float g, float b, float a);

    void SetSize(int width, int height);
}