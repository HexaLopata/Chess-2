using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class MonoFigure : MonoBehaviour
{
    public virtual FigureData Data
    {
        get
        {
            return _data;
        }
        set
        {
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

    [SerializeField] private Sprite _whiteSkin;
    [SerializeField] private Sprite _blackSkin;
    protected FigureData _data;
    protected Image _image;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    protected virtual void MoveToAnotherCell(CellBase cellBase)
    {
        if (CellBase != null)
            CellBase.Figure = null;
        cellBase.Figure = _data;
        CellBase = cellBase;
        OnBoardPosition = CellBase.OnBoardPosition;
    }
    public IEnumerator MoveToAnotherCellWithAnimation(CellBase cellBase)
    {
        MoveToAnotherCell(cellBase);
        var rectTransform = GetComponent<RectTransform>();
        var startPoint = rectTransform.localPosition;
        var endPoint = new Vector2(cellBase.RectTransform.localPosition.x + cellBase.RectTransform.rect.width / 2,
                                    cellBase.RectTransform.localPosition.y + cellBase.RectTransform.rect.height / 2);
        var direction = new Vector2(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        var roundedEndPoint = new Vector2Int(Convert.ToInt32(endPoint.x), Convert.ToInt32(endPoint.y));

        while (Convert.ToInt32(rectTransform.localPosition.x) != roundedEndPoint.x ||
               Convert.ToInt32(rectTransform.localPosition.y) != roundedEndPoint.y )
        {
            var x = rectTransform.localPosition.x;
            var y = rectTransform.localPosition.y;
            rectTransform.localPosition = new Vector2(x + direction.x / 15, y + direction.y / 15);
            yield return new WaitForSeconds(0.01f);
        }
        var cellPosition = CellBase.RectTransform.localPosition;
        // Переносим и выравниваем
        Vector2 newPosition = new Vector2(cellPosition.x + CellBase.RectTransform.rect.width / 2,
            cellPosition.y + CellBase.RectTransform.rect.height / 2);
        GetComponent<RectTransform>().localPosition = newPosition;
    }

    public void DestroyThisFigure()
    {
        if(CellBase != null)
            CellBase.Figure = null;
        Destroy(gameObject);
    }
}