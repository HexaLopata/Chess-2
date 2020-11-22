using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ExitSceneAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        PlayReturn();
    }

    public void PlayExit(Action callBack = null)
    {
        StartCoroutine(Exit(callBack));
    }

    public void PlayReturn()
    {
        StartCoroutine(Return());
    }
    
    private IEnumerator Exit(Action callBack = null)
    {
        GetComponent<Image>().color = Color.black;
        while (_rectTransform.localPosition.x < 0)
        {
            _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x + 40, _rectTransform.localPosition.y);
            yield return null;
        }

        callBack?.Invoke();
    }

    private IEnumerator Return()
    {
        GetComponent<Image>().color = Color.black;
        while (_rectTransform.localPosition.x > -3000)
        {
            _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x - 40, _rectTransform.localPosition.y);
            yield return null;
        }
    }
}
