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

    public AddonPoolBar(UIPoolBar poolbar)
    {
        _poolbar = poolbar;
        _rectTransform = poolbar.GetComponent<RectTransform>();        
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

    public void SetupWindow()
    {
        // This sets the bar position
        var bar = _poolbar.transform.GetChild(1).GetComponent<RectTransform>();
        bar.sizeDelta = new Vector2(0, 10);

        // This sets the Panel_OffensiveTarget windows height
        var panel = _rectTransform.parent.parent.parent.GetComponent<RectTransform>();
        panel.sizeDelta = new Vector2(panel.sizeDelta.x, panel.sizeDelta.y + 5);

        // This moves the Text_TargetName
        var textTargetName = _rectTransform.parent.parent.FindChild("Text_TargetName").GetComponent<RectTransform>();
        textTargetName.anchoredPosition = new Vector2(0, 20);
    }
}