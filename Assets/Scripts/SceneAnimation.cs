using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        GetComponent<Image>().color = Color.black;
        while (_rectTransform.localPosition.x < 4000)
        {
            _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x + 40, _rectTransform.localPosition.y);
            yield return null;
        }
    }
    
    public IEnumerator Close()
    {
        GetComponent<Image>().color = Color.black;
        while (_rectTransform.localPosition.x > 0)
        {
            _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x - 40, _rectTransform.localPosition.y);
            yield return null;
        }
    }
}
