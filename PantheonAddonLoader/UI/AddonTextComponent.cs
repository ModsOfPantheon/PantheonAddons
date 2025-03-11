using Il2CppTMPro;
using PantheonAddonFramework.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.UI;

public class AddonTextComponent : IAddonTextComponent
{
    private TextMeshProUGUI Text { get; }

    public AddonTextComponent(TextMeshProUGUI text)
    {
        Text = text;
    }

    public string GetText()
    {
        return Text.text;
    }

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void SetSize(float width, float height)
    {
        // It's very frustrating that they're not using a later version of TMP, in the current version
        // autoSizeTextContainer only sets the size on initial creation, but in later versions it sets it
        // whenever the text is updated :(
        // For now lets just hack this in
        Text.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    public void SetFontSize(float fontSize)
    {
        Text.fontSize = fontSize;
    }

    public float GetFontSize()
    {
        return Text.fontSize;
    }

    public void SetFontColor(byte red, byte green, byte blue, byte alpha)
    {
        Text.color = new Color32(red, green, blue, alpha);
    }

    public void SetFontColor(float red, float green, float blue, float alpha)
    {
        Text.color = new Color(red, green, blue, alpha);    
    }

    public void Enable(bool enabled)
    {
        Text.gameObject.SetActive(enabled);
    }

    public void Destroy()
    {
        Object.Destroy(Text.gameObject);
    }
}