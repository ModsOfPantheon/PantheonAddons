using PantheonAddonFramework.AddonComponents;
using UnityEngine;
using UnityEngine.Bindings;

namespace PantheonAddonLoader.AddonComponents;

public class CustomAssetManager : ICustomAssetManager
{
    private readonly Dictionary<string, Texture2D> _imageAssets = new();

    public Texture2D? GetSprite(string key)
    {
        return _imageAssets.GetValueOrDefault(key);
    }
    
    public string LoadSprite(string filePath)
    {
        if (_imageAssets.TryGetValue(filePath, out var texture))
        {
            return filePath;
        }

        texture = LoadTextureFromFile(filePath);
        _imageAssets.Add(filePath, texture);
        
        return filePath;
    }

    private static Texture2D LoadTextureFromFile(string filePath)
    {
        var imageAsBytes = File.ReadAllBytes(filePath);
        var image = new Texture2D(2, 2);

        unsafe
        {
            var intPtr = UnityEngine.Object.MarshalledUnityObject.MarshalNotNull(image);

            fixed (byte* ptr = imageAsBytes)
            {
                var managedSpanWrapper = new ManagedSpanWrapper(ptr, imageAsBytes.Length);

                ImageConversion.LoadImage_Injected(intPtr, ref managedSpanWrapper, false);
            }
        }

        return image;
    }
}