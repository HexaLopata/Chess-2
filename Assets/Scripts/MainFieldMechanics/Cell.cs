using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public MainFieldFigure Figure { get; set; }
    public Vector2Int OnBoardPosition { get; set; }
    public CellState State { get; set; } = CellState.NotActive;
    public RectTransform RectTransform { get; private set; }

    [SerializeField] private Sprite _normal;
    [SerializeField] private Sprite _active;
    [SerializeField] private Sprite _enemy;
    private Image _image;
    private Field _field;

    private void Awake()
    {
        _field = GetComponentInParent<Field>();
        RectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.sprite = _normal;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var field = GetComponentInParent<Field>();
        field.TurnManager.OnCellClick(this);
    }

    /// <summary>
    /// Делает клетку активной для совершения хода на нее текущей фигурой
    /// </summary>
    public void Activate()
    {
        State = CellState.Active;
        if (_field.TurnManager.SelectedFigure != Figure)
        {
            if (Figure != null)
            {
                if (_field.TurnManager.CurrentTurn != Figure.Data.Team)
                    _image.sprite = _enemy;
            }
            else
                _image.sprite = _active;
        }
    }

    /// <summary>
    /// Делает клетку не активной для совершения хода на нее текущей фигурой
    /// </summary>
    public void Deactivate()
    {
        State = CellState.NotActive;
        _image.sprite = _normal;
    }
}

public enum CellState
{
    NotActive,
    Active,
}