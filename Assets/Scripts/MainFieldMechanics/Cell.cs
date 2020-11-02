using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public MainFieldFigure Figure { get; set; }
    public Vector2 OnBoardPosition { get; set; }
    public CellState State { get; set; } = CellState.NotActive;
    public RectTransform RectTransform { get; private set; }

    private Color _color;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        _color = GetComponent<Image>().color;
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
        GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// Делает клетку не активной для совершения хода на нее текущей фигурой
    /// </summary>
    public void Deactivate()
    {
        State = CellState.NotActive;
        GetComponent<Image>().color = _color;
    }
}

public enum CellState
{
    NotActive,
    Active,
}