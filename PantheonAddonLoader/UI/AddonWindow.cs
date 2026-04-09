using Il2Cpp;
using Il2CppTMPro;
using PantheonAddonFramework.UI;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PantheonAddonLoader.UI;

public class AddonWindow : IAddonWindow
{
    private UIWindowPanel _window;
    private RectTransform _rectTransform;
    
    public AddonWindow(UIWindowPanel window)
    {
        _window = window;
        _rectTransform = window.GetComponent<RectTransform>();
    }

    public float Height => _rectTransform.sizeDelta.y;
    public float Width =>  _rectTransform.sizeDelta.x;
    public float X => _rectTransform.transform.position.x;
    public float Y => _rectTransform.transform.position.y;

    public void SetHeight(float height)
    {
        _rectTransform.sizeDelta = new Vector2(Width, height);
    }

    public void SetWidth(float newWidth)
    {
        _rectTransform.sizeDelta = new Vector2(newWidth, Height);
    }

    public void SetPosition(float newX, float newY)
    {
        _rectTransform.transform.position = new Vector2(newX, newY);
    }

    public IAddonImageComponent AddImageComponent(string objectName)
    {
        return ComponentFactory.CreateImageComponent(_rectTransform, AddonLoader.CustomAssetManager, objectName);
    }

    public IAddonTextComponent AddTextComponent(string initialText)
    {
        return ComponentFactory.CreateTextComponent(_rectTransform, objectName: "Text", initialText);
    }

    // TODO: Set this up from scratch instead of copying from an existing window
    public IAddonWindow AddResizeHandle(int maxWidth, int maxHeight, int minWidth, int minHeight)
    {
        var mainChatWindow = UIChatWindows.Instance.mainWindow;
        var resizeHandle = mainChatWindow.GetComponentInChildren<UIResizeHandle>();
        
        var resizeCopy = Object.Instantiate(resizeHandle, resizeHandle.transform.position, resizeHandle.transform.rotation, _rectTransform);
        var copyHandle = resizeCopy.GetComponent<UIResizeHandle>();
        copyHandle.ContainerRect = _rectTransform;
        copyHandle.MaxSize = new Vector2(maxWidth, maxHeight);
        copyHandle.MinSize = new Vector2(minWidth, minHeight);
        
        var copyRect = resizeCopy.GetComponent<RectTransform>();
        copyRect.pivot = new Vector2(1, 0);
        copyRect.sizeDelta = new Vector2(25, 25);
        copyRect.anchoredPosition = new Vector2(-5, 4);
        
        return this;
    }

    public void Enable(bool enabled)
    {
        _window.gameObject.SetActive(enabled);
    }

    public void Destroy()
    {
        Object.Destroy(_window.gameObject);
    }

    public ILayoutObject AddVerticalLayout()
    {
        var child = new GameObject("Layout");
        child.transform.SetParent(_rectTransform);
        var layout = child.AddComponent<VerticalLayoutGroup>();
        return new LayoutObject(layout);
    }
}