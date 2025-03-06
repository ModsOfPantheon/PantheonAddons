using Il2Cpp;
using MelonLoader;
using PantheonAddonFramework.AddonComponents;
using PantheonAddonFramework.UI;
using PantheonAddonLoader.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PantheonAddonLoader.AddonComponents;

public class CustomUI : ICustomUI
{
    public IAddonWindow CreateWindow(string name, int initialWidth, int initialHeight)
    {
        var midPanel = UIPanelRoots.Instance.Mid;
        var uiWindowPanel = SetupCustomWindow(name, midPanel.transform, new Vector2(initialWidth, initialHeight));

        return new AddonWindow(uiWindowPanel);
    }
    
    // TODO: Create windows from scratch instead of copying an existing window
    // TODO: Move some of this to the AddonWindow class, e.g., adding close button
    private static UIWindowPanel SetupCustomWindow(string windowName, Transform midPanel, Vector2 initialSize)
    {
        var tutorialPopup = midPanel.GetComponentInChildren<UITutorialPopup>();
        var buttonToCopy = tutorialPopup.transform.GetChild(0);
        var imageToCopy = tutorialPopup.GetComponent<Image>();
        var gameObject = new GameObject(windowName);
        gameObject.transform.SetParent(midPanel);
        gameObject.layer = Layers.UI;
        
        var rectTransform = gameObject.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        rectTransform.sizeDelta = initialSize;
        
        var canvasRenderer = gameObject.AddComponent<CanvasRenderer>();
        
        var canvasGroup = gameObject.AddComponent<CanvasGroup>();
        var uiDraggable = gameObject.AddComponent<UIDraggable>();
        var uiWindowPanel = gameObject.AddComponent<UIWindowPanel>();
        
        uiWindowPanel.CanvasGroup = canvasGroup;
        uiWindowPanel._displayName = "";

        uiDraggable._windowPanel = uiWindowPanel;
        
        var layout = gameObject.AddComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset(14, 14, 14, 14);

        var image = gameObject.AddComponent<Image>();
        image.type = Image.Type.Sliced;
        image.sprite = imageToCopy.sprite;
        
        var buttonCopy = GameObject.Instantiate(buttonToCopy, buttonToCopy.transform.position,
            buttonToCopy.transform.rotation, uiWindowPanel.transform);

        var buttonRect = buttonCopy.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(30, 30);
        buttonRect.anchoredPosition = new Vector2(-13.5f, -12);
        
        var buttonComponent = buttonCopy.GetComponent<Button>();
        buttonComponent.onClick = new Button.ButtonClickedEvent();
        buttonComponent.onClick.RemoveAllListeners();
        buttonComponent.onClick.AddCall(new InvokableCall(new Action(() =>
        {
            uiWindowPanel.Hide();
        })));
        
        buttonComponent.onClick.AddCall(new InvokableCall(new Action(() =>
        {
            buttonCopy.GetComponent<UI_Audio_Function>().Play_UI_Generic_Click();
        })));
        
        uiWindowPanel.Show();

        return uiWindowPanel;
    }
}