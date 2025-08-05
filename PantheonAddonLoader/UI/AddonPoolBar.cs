using Il2Cpp;
using Il2CppTMPro;
using PantheonAddonFramework.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.UI;

public class AddonPoolBar : IAddonPoolBar
{
    private UIPoolBar _poolbar;
    private RectTransform _rectTransform;

    public IAddonRectTransform Bar { get; }
    public IAddonRectTransform Panel { get; }
    public IAddonRectTransform TargetNameText { get; }

    public AddonPoolBar(UIPoolBar poolbar)
    {
        _poolbar = poolbar;
        _rectTransform = poolbar.GetComponent<RectTransform>();

        Bar = new AddonRectTransform(_poolbar.transform.GetChild(1).GetComponent<RectTransform>());
        Panel = new AddonRectTransform(_rectTransform.parent.parent.parent.GetComponent<RectTransform>());
        TargetNameText = new AddonRectTransform(_rectTransform.parent.parent.FindChild("Text_TargetName").GetComponent<RectTransform>());
    }

    public IAddonTextComponent AddTextComponent(string initialText)
    {
        var go = new GameObject("PoolBarText");
        go.transform.SetParent(_poolbar.transform);

        var textComponent = go.AddComponent<TextMeshProUGUI>();
        textComponent.text = initialText;
        textComponent.fontSize = 14;
        textComponent.fontStyle = Il2CppTMPro.FontStyles.Bold;
        textComponent.alignment = TextAlignmentOptions.Center;

        var rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);

        return new AddonTextComponent(textComponent);
    }

    public void Enable(bool enabled)
    {
        _poolbar.gameObject.SetActive(enabled);
    }

    public void Destroy()
    {
        Object.Destroy(_poolbar);
    }
}