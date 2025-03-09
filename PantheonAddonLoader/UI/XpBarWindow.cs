using Il2Cpp;
using PantheonAddonFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PantheonAddonLoader.UI;

public class XpBarWindow : AddonWindow, IXpBarWindow
{
    private UIWindowPanel _uiWindowPanel;
    private Image _backgroundImage;
    private Image _sliderImage;
    
    public XpBarWindow(UIWindowPanel window) : base(window)
    {
        _uiWindowPanel = window;
        _backgroundImage = window.transform.GetChild(0).GetComponent<Image>();
        _sliderImage = window.transform.GetChild(1).GetComponent<Image>();
    }

    public void ShowTicks(bool show)
    {
        var ticksHolder = _uiWindowPanel.transform.GetChild(2);
        ticksHolder.gameObject.SetActive(show);
    }

    public void SetBackgroundColour(float red, float green, float blue, float alpha)
    {
        _backgroundImage.color = new Color(red, green, blue, alpha);
    }

    public void SetSliderColour(float red, float green, float blue, float alpha)
    {
        _sliderImage.color = new Color(red, green, blue, alpha);
    }
}