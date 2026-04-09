using Il2Cpp;
using Il2CppTMPro;
using PantheonAddonLoader.AddonComponents;
using PantheonAddonFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PantheonAddonLoader.UI;

public static class ComponentFactory
{
    public static IAddonImageComponent CreateImageComponent(Transform parent, CustomAssetManager assetManager, string objectName)
    {
        var go = new GameObject(objectName);
        go.transform.SetParent(parent);

        var imageComponent = go.AddComponent<Image>();

        return new AddonImageComponent(imageComponent, assetManager);
    }

    public static IAddonTextComponent CreateTextComponent(Transform parent, string objectName, string initialText)
    {
        var go = new GameObject(objectName);
        go.transform.SetParent(parent);
        
        var textComponent = go.AddComponent<TextMeshProUGUI>();
        textComponent.text = initialText;
        textComponent.fontSize = 18;
        textComponent.alignment = TextAlignmentOptions.Center;
        
        var rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        
        return new AddonTextComponent(textComponent);
    }
}
