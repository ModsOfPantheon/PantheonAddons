using PantheonAddonFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PantheonAddonLoader.UI;

public class AddonImageComponent : IAddonImageComponent
{
    private readonly ICustomAssetManager _customAssetManager;
    private readonly Image _image;

    public AddonImageComponent(Image image, ICustomAssetManager customAssetManager)
    {
        _customAssetManager = customAssetManager;
        _image = image;
    }

    public void SetSpriteFromFile(string filePath)
    {
        var texture = _customAssetManager.GetSprite(filePath);
        
        _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    public void SetColour(byte r, byte g, byte b, byte a)
    {
        _image.color = new Color32(r, g, b, a);
    }

    public void SetColour(float r, float g, float b, float a)
    {
        _image.color = new Color(r, g, b, a);
    }

    public void SetSize(int width, int height)
    {
        _image.rectTransform.sizeDelta = new Vector2(width, height);
    }
}