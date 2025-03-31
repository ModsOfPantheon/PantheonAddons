using UnityEngine;

namespace PantheonAddonLoader;

public interface ICustomAssetManager
{
    Texture2D GetSprite(string filePath);
}