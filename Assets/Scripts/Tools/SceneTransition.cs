using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Анимация для перехода на другую сцену
/// </summary>
public class SceneTransition : MonoBehaviour
{
    /// <summary>
    /// На столько пикселей будет перемещаться изображение за один шаг
    /// </summary>
    [SerializeField] private int _step = 40;
    /// <summary>
    /// Будет ли переход открываться слева
    /// </summary>
    [SerializeField] private bool _leftDirection = true;
    /// <summary>
    /// Будет ли переход проигрывать анимацию открытия при старте сцены
    /// </summary>
    [SerializeField] private bool _autoPlay = false;
    /// <summary>
    /// Раз в столько секунд будет происходить шаг
    /// </summary>
    [SerializeField] private float _stepPeriod = 0.015f;
    /// <summary>
    /// На какой позиции остановится анимация
    /// </summary>
    [SerializeField] private int _endPosition = 3000;
    private RectTransform _rectTransform;
    private Coroutine _open;
    private Coroutine _close;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        var image = GetComponent<Image>();
        image.enabled = true;
        image.color = Color.black;
        if(_autoPlay)
            PlayOpen();
    }

    /// <summary>
    /// Запускает анимацию закрытия и выполняет переданный метод
    /// </summary>
    /// <param name="callBack"></param>
    public void PlayClose(Action callBack = null)
    {
        if(_close == null)
            _close = StartCoroutine(Close(callBack));
        else
            callBack?.Invoke();
    }

    /// <summary>
    /// Проигрывает анимацию открытия сцены и выполняет переданный метод
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
    /// Проигрывает анимацию закрытия сцены и выполняет переданный метод
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
                yield return new WaitForSeconds(_stepPeriod);
            }
        }
        else
        {
            while (_rectTransform.localPosition.x > 0)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x - _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return new WaitForSeconds(_stepPeriod);
            }
        }

        callBack?.Invoke();
        _close = null;
    }

    /// <summary>
    /// Проигрывает анимацию открытия сцены и выполняет переданный метод
    /// </summary>
    /// <param name="callBack"></param>
    /// <returns></returns>
    public IEnumerator Open(Action callBack = null)
    {
        if (_leftDirection)
        {
            while (_rectTransform.localPosition.x > -_endPosition)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x - _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return new WaitForSeconds(_stepPeriod);
            }
        }
        else
        {
            while (_rectTransform.localPosition.x < _endPosition)
            {
                var localPosition = _rectTransform.localPosition;
                localPosition = new Vector2(localPosition.x + _step, localPosition.y);
                _rectTransform.localPosition = localPosition;
                yield return new WaitForSeconds(_stepPeriod);
            }
        }

        callBack?.Invoke();
        _open = null;
    }
}
