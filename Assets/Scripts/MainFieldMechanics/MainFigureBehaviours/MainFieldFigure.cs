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
                _image.sprite = _blackSkin;
            else
                _image.sprite = _whiteSkin;
            _data = value;
        } 
    }  

    public Cell Cell { get; set; }
    public Vector2 OnBoardPosition { get; set; }

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

    public void MoveToAnotherCell(Cell cell)
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
        _rectTransform.localPosition = newPosition;
    }

    public virtual Cell[,] GetRelevantTurn(Cell[,] cells)
    {
        return cells;
    }
}
