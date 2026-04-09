using PantheonAddonFramework.UI;
using UnityEngine.UI;

namespace PantheonAddonLoader.UI;

public class LayoutObject : ILayoutObject
{
    private readonly LayoutGroup _layoutGroup;

    public LayoutObject(LayoutGroup layoutGroup)
    {
        _layoutGroup = layoutGroup;
    }

    public IAddonImageComponent AddImageComponent(string objectName)
    {
        return ComponentFactory.CreateImageComponent(_layoutGroup.transform, AddonLoader.CustomAssetManager, objectName);
    }

    public IAddonTextComponent AddTextComponent(string initialText)
    {
        return ComponentFactory.CreateTextComponent(_layoutGroup.transform, objectName: "Text", initialText);
    }

    public void SetSpacing(float spacing)
    {
        if (_layoutGroup is HorizontalOrVerticalLayoutGroup hvlg)
        {
            hvlg.spacing = spacing;
        }
    }
}