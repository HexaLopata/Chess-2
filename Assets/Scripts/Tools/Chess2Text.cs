using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Chess2Text : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private string _text = "DEFAULT TEXT";
    [SerializeField] private float _fontSize = 3;
    [SerializeField] private float _leftBias = 0.5f;
    [SerializeField] private float _space = 0.5f;
    
    [Header("Init")]
    [SerializeField] private Image emptyImage;
    
    [Header("Letter Sprites")] 
    [SerializeField] private Sprite a;
    [SerializeField] private Sprite b;
    [SerializeField] private Sprite c;
    [SerializeField] private Sprite d;
    [SerializeField] private Sprite e;
    [SerializeField] private Sprite f;
    [SerializeField] private Sprite g;
    [SerializeField] private Sprite h;
    [SerializeField] private Sprite i;
    [SerializeField] private Sprite j;
    [SerializeField] private Sprite k;
    [SerializeField] private Sprite l;
    [SerializeField] private Sprite m;
    [SerializeField] private Sprite n;
    [SerializeField] private Sprite o;
    [SerializeField] private Sprite p;
    [SerializeField] private Sprite q;
    [SerializeField] private Sprite r;
    [SerializeField] private Sprite s;
    [SerializeField] private Sprite t;
    [SerializeField] private Sprite u;
    [SerializeField] private Sprite v;
    [SerializeField] private Sprite w;
    [SerializeField] private Sprite x;
    [SerializeField] private Sprite y;
    [SerializeField] private Sprite z;
    [SerializeField] private Sprite one;
    [SerializeField] private Sprite two;
    [SerializeField] private Sprite three;
    [SerializeField] private Sprite four;
    [SerializeField] private Sprite five;
    [SerializeField] private Sprite six;
    [SerializeField] private Sprite seven;
    [SerializeField] private Sprite eight;
    [SerializeField] private Sprite nine;
    [SerializeField] private Sprite zero;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            UpdateText();
        }
    }

    public float FontSize
    {
        get => _fontSize;
        set
        {
            _fontSize = value;
            UpdateText();
        }
    }

    private Dictionary<char, Sprite> charAndSprites = new Dictionary<char, Sprite>();
    private float currentWidth;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        InitDictionary();
        UpdateText();
    }

    private void InitDictionary()
    {
        charAndSprites.Add('a', a);
        charAndSprites.Add('b', b);
        charAndSprites.Add('c', c);
        charAndSprites.Add('d', d);
        charAndSprites.Add('e', e);
        charAndSprites.Add('f', f);
        charAndSprites.Add('g', g);
        charAndSprites.Add('h', h);
        charAndSprites.Add('i', i);
        charAndSprites.Add('j', j);
        charAndSprites.Add('k', k);
        charAndSprites.Add('l', l);
        charAndSprites.Add('m', m);
        charAndSprites.Add('n', n);
        charAndSprites.Add('o', o);
        charAndSprites.Add('p', p);
        charAndSprites.Add('q', q);
        charAndSprites.Add('r', r);
        charAndSprites.Add('s', s);
        charAndSprites.Add('t', t);
        charAndSprites.Add('u', u);
        charAndSprites.Add('v', v);
        charAndSprites.Add('w', w);
        charAndSprites.Add('x', x);
        charAndSprites.Add('y', y);
        charAndSprites.Add('z', z);
        charAndSprites.Add('1', one);
        charAndSprites.Add('2', two);
        charAndSprites.Add('3', three);
        charAndSprites.Add('4', four);
        charAndSprites.Add('5', five);
        charAndSprites.Add('6', six);
        charAndSprites.Add('7', seven);
        charAndSprites.Add('8', eight);
        charAndSprites.Add('9', nine);
        charAndSprites.Add('0', zero);
    }
    
    private void UpdateText()
    {
        currentWidth = 0;
        _text = _text.ToLower();
        foreach (var charecter in _text)
        {
            Sprite sprite;

            _rectTransform.sizeDelta = new Vector3(0, 0);
            if (charAndSprites.TryGetValue(charecter, out sprite))
            {
                _rectTransform.localScale = new Vector3(_fontSize, _fontSize);
                var image = Instantiate(emptyImage, transform);
                image.sprite = sprite;
                image.SetNativeSize();
                image.rectTransform.localPosition = new Vector3(currentWidth, 0);
                // Отнимаем leftBias, чтобы текст был более плотным или широким
                currentWidth += (image.rectTransform.rect.width) - _leftBias;
                _rectTransform.sizeDelta = new Vector3( currentWidth, image.rectTransform.rect.height);
            }

            if (charecter == ' ')
            {
                currentWidth += _space * _fontSize;
                _rectTransform.sizeDelta = new Vector3( currentWidth, _rectTransform.sizeDelta.y);
            }
        }
    }
}