using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(Image))]
public class MainFieldFigure : MonoBehaviour, IPointerClickHandler
{
    public FigureData Data
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

    public Cell Cell { get; set; }
    public Vector2Int OnBoardPosition { get; set; }

    [SerializeField] private Sprite _whiteSkin;
    [SerializeField] private Sprite _blackSkin;
    private FigureData _data;
    private Image _image;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var field = GetComponentInParent<Field>();
        field.TurnManager.OnFigureClick(this);
    }

    public virtual void MoveToAnotherCell(Cell cell)
    {
        if (Cell != null)
            Cell.Figure = null;
        Cell = cell;
        Cell.Figure = this;
        OnBoardPosition = Cell.OnBoardPosition;
        var cellPosition = Cell.RectTransform.localPosition;
        // Переносим и выравниваем
        Vector2 newPosition = new Vector2(cellPosition.x + Cell.RectTransform.rect.width / 2,
                                          cellPosition.y + Cell.RectTransform.rect.height / 2);
        GetComponent<RectTransform>().localPosition = newPosition;
    }

    public void DestroyThisFigure()
    {
        if(Cell != null)
            Cell.Figure = null;
        Destroy(gameObject);
    }

    public virtual Cell[] GetRelevantTurn(Cell[,] cells)
    {
        return new Cell[0];
    }
}