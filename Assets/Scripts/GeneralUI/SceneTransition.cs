using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    /// <summary>
    /// На столько пикселей будет перемещаться изображение за один шаг
    /// </summary>
    [SerializeField] private int _step = 40;
    [SerializeField] private bool _leftDirection = true;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        PlayOpen();
    }

    public void PlayClose(Action callBack = null)
    {
        StartCoroutine(Close(callBack));
    }

    /// <summary>
    /// Проигрывает анимацию открытия сцены
    /// </summary>
    /// <param name="callBack"></param>
    public void PlayOpen(Action callBack = null)
    {
        StartCoroutine(Open());
    }
    
    /// <summary>
    /// Проигрывает анимацию закрытия сцены
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator Close(Action callBack = null)
    {
        GetComponent<Image>().color = Color.black;
        if (_leftDirection)
        {
            while (_rectTransform.localPosition.x < 0)
            {
                _rectTransform.localPosition =
                    new Vector2(_rectTransform.localPosition.x + _step, _rectTransform.localPosition.y);
                yield return null;
            }
        }
        else
        {
            while (_rectTransform.localPosition.x > 0)
            {
                _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x - _step, _rectTransform.localPosition.y);
                yield return null;
            }
        }

        callBack?.Invoke();
    }

    public IEnumerator Open(Action callBack = null)
    {
        GetComponent<Image>().color = Color.black;
        if (_leftDirection)
        {
            while (_rectTransform.localPosition.x > -3000)
            {
                _rectTransform.localPosition =
                    new Vector2(_rectTransform.localPosition.x - _step, _rectTransform.localPosition.y);
                yield return null;
            }
        }
        else
        {
            while (_rectTransform.localPosition.x < 3000)
            {
                _rectTransform.localPosition = new Vector2(_rectTransform.localPosition.x + _step, _rectTransform.localPosition.y);
                yield return null;
            }
        }

        callBack?.Invoke();
    }
}
