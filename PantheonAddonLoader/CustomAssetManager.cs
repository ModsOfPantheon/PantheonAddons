using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using UnityEngine.Bindings;

namespace PantheonAddonLoader;

public class CustomAssetManager : ICustomAssetManager
{
    private Dictionary<string, Texture2D> _imageAssets = new();
    
    public Texture2D GetSprite(string filePath)
    {
        if (_imageAssets.TryGetValue(filePath, out var texture))
        {
            return texture;
        }

        texture = LoadTextureFromFile(filePath);
        _imageAssets.Add(filePath, texture);
        
        return texture;
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