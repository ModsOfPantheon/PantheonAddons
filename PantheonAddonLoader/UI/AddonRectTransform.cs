using PantheonAddonFramework.UI;
using UnityEngine;

namespace PantheonAddonLoader.UI;

public class AddonRectTransform : IAddonRectTransform
{
    private readonly RectTransform _rectTransform;
    
    public AddonRectTransform(RectTransform rectTransform)
    {
        _rectTransform = rectTransform;
    }

    public (float Width, float Height) GetSizeDelta()
    {
        return (_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y);
    }

    public void SetSize(float width, float height)
    {
        _rectTransform.sizeDelta = new Vector2(width, height);
    }

    public (float Width, float Height) GetAnchoredPosition()
    {
        return (_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y);
    }

    public void SetAnchoredPosition(float width, float height)
    {
        _rectTransform.anchoredPosition = new Vector2(width, height);
    }
}