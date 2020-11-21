using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScalingButton : Button
{
    private Image _image;
    private Sprite _normalSprite;

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

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _image.sprite =  _normalSprite;
        _image.SetNativeSize();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _image.sprite = spriteState.pressedSprite;
        _image.SetNativeSize();
    }
}
