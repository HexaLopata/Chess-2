﻿using System;
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
    [SerializeField] private bool _autoPlay = false;
    private RectTransform _rectTransform;
    private Coroutine _open;
    private Coroutine _close;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        GetComponent<Image>().color = Color.black;
        if(_autoPlay)
            PlayOpen();
    }

    public void PlayClose(Action callBack = null)
    {
        if(_close == null)
            _close = StartCoroutine(Close(callBack));
        else
            callBack?.Invoke();
    }

    /// <summary>
    /// Проигрывает анимацию открытия сцены
    /// </summary>
    /// <param name="callBack"></param>
    public void PlayOpen(Action callBack = null)
    {
        if(_open == null)
            _open = StartCoroutine(Open(callBack));
        else
            callBack?.Invoke();
    }
    
    /// <summary>
    /// Проигрывает анимацию закрытия сцены
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator Close(Action callBack = null)
    {
        if (_leftDirection)
        {
            while (_rectTransform.localPosition.x < 0)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x + _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return null;
            }
        }
        else
        {
            while (_rectTransform.localPosition.x > 0)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x - _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return null;
            }
        }

        callBack?.Invoke();
        _close = null;
    }

    public IEnumerator Open(Action callBack = null)
    {
        if (_leftDirection)
        {
            while (_rectTransform.localPosition.x > -3000)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x - _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return null;
            }
        }
        else
        {
            while (_rectTransform.localPosition.x < 3000)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x + _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return null;
            }
        }

        callBack?.Invoke();
        _open = null;
    }
}
