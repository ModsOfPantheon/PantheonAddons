using Il2CppTMPro;
using PantheonAddonFramework.Components;
using UnityEngine;

namespace PantheonAddonLoader.Components;

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
}