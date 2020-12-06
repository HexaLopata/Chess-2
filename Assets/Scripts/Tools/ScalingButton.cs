using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������, ��� ������� ������� �� ������ ���������� ������ ������� �������
/// </summary>
public class ScalingButton : Button
{
    private Image _image;
    private Sprite _normalSprite;

    /// <summary>
    /// ����������� ������
    /// </summary>
    public Sprite NormalSprite
    {
        get => _normalSprite;
        set => _normalSprite = value;
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _normalSprite = _image.sprite;
    }

    /// <summary>
    /// ������ �� ��, ��� � ������� ������, �� ����� ������ �� ������ ������� � �������� ������ �� ������� �������
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _image.sprite = _normalSprite;
        _image.SetNativeSize();
    }

    /// <summary>
    /// ������ �� ��, ��� � ������� ������, �� ����� ������ �� ������ ������� � �������� ������ �� ������� �������
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _image.sprite = spriteState.pressedSprite;
        _image.SetNativeSize();
    }
}
