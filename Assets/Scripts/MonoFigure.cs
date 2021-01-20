using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Абстрактный класс для фигур главного поля и поля битвы
/// </summary>
public abstract class MonoFigure : MonoBehaviour
{
    // Вся общая информация о фигуре
    public virtual FigureData Data
    {
        get
        {
            return _data;
        }
        set
        {
            // Обновляет скин, если изменилась команда
            if (value.Team == Team.Black)
            {
                _image.sprite = _blackSkin;
                _rectTransform.rotation = new Quaternion(0, 0, 90, 0);
            }
            else
            {
                _image.sprite = _whiteSkin;
                _rectTransform.rotation = new Quaternion(0, 0, 0, 0);
            }
            
            _data = value;
        } 
    }  
    public CellBase CellBase { get; set; }
    public Vector2Int OnBoardPosition { get; set; }
    public bool King => _king;

    private RectTransform _rectTransform;
    private AudioSource _movementSound;
    private bool _isFirstMovement = true;
    private bool _isMoving = false;

    [SerializeField] private Sprite _whiteSkin;
    [SerializeField] private Sprite _blackSkin;
    [SerializeField] private bool _king;
    [SerializeField] private const float _moveTime = 0.25f;

    protected FigureData _data;
    protected Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _movementSound = GetComponent<AudioSource>();
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Перемещает фигуру на переданную клетку, обновляет необходимые данные и выравнивает фигуру
    /// </summary>
    /// <param name="cellBase"></param>
    public virtual void MoveToAnotherCell(CellBase cellBase)
    {
        if (CellBase != null)
            CellBase.Figure = null;
        cellBase.Figure = _data;
        CellBase = cellBase;
        OnBoardPosition = CellBase.OnBoardPosition;
    }

    /// <summary>
    /// Делает то же, что и MoveToAnotherCell, но с анимацией
    /// </summary>
    /// <param name="cellBase"></param>
    /// <returns></returns>
    public virtual IEnumerator MoveToAnotherCellWithAnimation(CellBase cellBase)
    {
        while(_isMoving)
            yield return new WaitForSeconds(_moveTime); 

        _isMoving = true;
        MoveToAnotherCell(cellBase);
        var rectTransform = GetComponent<RectTransform>();
        var startPosition = rectTransform.localPosition;
        var targetPosition = new Vector2(cellBase.RectTransform.localPosition.x + cellBase.RectTransform.rect.width / 2,
                                    cellBase.RectTransform.localPosition.y + cellBase.RectTransform.rect.height / 2);

        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while(fraction < 1f) 
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / _moveTime); 
            rectTransform.localPosition = Vector2.Lerp(startPosition, targetPosition, fraction);
            yield return null; 
        }

        if (!_isFirstMovement)
        {
            _movementSound.Play();
        }
        _isFirstMovement = false;
        _isMoving = false;
    }

    /// <summary>
    /// Уничтожает фигуру и очищает информацию о себе на клетке, на которой сейчас стоит фигура
    /// </summary>
    public void DestroyThisFigure()
    {
        if(CellBase != null)
            CellBase.Figure = null;
        Destroy(gameObject);
    }
}