namespace PantheonAddonFramework.UI;

public interface IAddonRectTransform
{
    (float Width, float Height) GetSizeDelta();
    void SetSize(float width, float height);
    (float Width, float Height) GetAnchoredPosition();
    void SetAnchoredPosition(float width, float height);
}