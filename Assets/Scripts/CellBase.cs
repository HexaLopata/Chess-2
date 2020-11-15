using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform)),]
public abstract class CellBase : MonoBehaviour, IPointerClickHandler
{
    public virtual FigureData Figure { get; set; }
    public Vector2Int OnBoardPosition { get; set; }
    public CellState State { get; protected set; } = CellState.NotActive;
    public RectTransform RectTransform { get; private set; }

    [SerializeField] protected Sprite _normal;
    [SerializeField] protected Sprite _active;
    [SerializeField] protected Sprite _enemy;
    protected Image _image;
    protected FieldBase _field;

    private void Awake()
    {
        _field = GetComponentInParent<FieldBase>();
        RectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _image.sprite = _normal;
    }

    public abstract void OnPointerClick(PointerEventData eventData);

    /// <summary>
    /// Делает клетку активной для совершения хода на нее текущей фигурой
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// Делает клетку не активной для совершения хода на нее текущей фигурой
    /// </summary>
    public abstract void Deactivate();
}

public enum CellState
{
    NotActive,
    Active,
}