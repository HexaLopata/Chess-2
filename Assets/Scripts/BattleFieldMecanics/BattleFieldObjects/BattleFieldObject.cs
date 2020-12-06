using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Базовый класс для объектов поля битвы
/// </summary>
[RequireComponent(typeof(Image), typeof(RectTransform))]
public abstract class BattleFieldObject : MonoBehaviour, IPointerClickHandler
{
    public Team Team
    {
        get => _team;
        set
        {
            if (value == Team.Black)
            {
                _image.sprite = _blackSkin;
                _rectTransform.rotation = new Quaternion(0, 0, 180, 0);
            }
            else
            {
                _image.sprite = _whiteSkin;
                _rectTransform.rotation = new Quaternion(0, 0, 0, 0);
            }

            _team = value;
        }
    }
    public Vector2Int OnBoardPosition { get; set; }
    public BattleField BattleField { get; private set; }
    public BattleFieldCell Cell
    {
        get => _cell;
        set
        {
            if (value != null)
            {
                BattleField = value.BattleField;
                BattleField.BattleController.onSwitchTurn.AddListener(Execute);
            }

            _cell = value;
        }
    }
    public Image Image => _image;
    public Sprite WhiteSkin => _whiteSkin;
    public Sprite BlackSkin => _blackSkin;
    
    private BattleFieldCell _cell;
    private Team _team;
    private RectTransform _rectTransform;
    protected Image _image;

    [SerializeField] private Sprite _whiteSkin;
    [SerializeField] private Sprite _blackSkin;

    /// <summary>
    /// Может ли фигура встать на клетку с этим объектом
    /// </summary>
    /// <param name="figure"></param>
    /// <returns></returns>
    public abstract BarrierType CanThisFigureToCross(BattleFieldFigure figure);
    /// <summary>
    /// Может ли фигура атаковать клетку с этим объектом
    /// </summary>
    /// <param name="figure"></param>
    /// <returns></returns>
    public abstract BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure);
    /// <summary>
    /// Выполняется при нанесении урона по этому объекту
    /// </summary>
    /// <param name="attacker"></param>
    public abstract void TakeDamage(BattleFieldFigure attacker);
    /// <summary>
    /// Выполняется при нанесении урона по этому объекту
    /// </summary>
    /// <param name="attacker"></param>
    public abstract void TakeDamage(int damage);
    /// <summary>
    /// Выполняется, когда на клетку с этим объектом встает фигура
    /// </summary>
    /// <param name="visitor"></param>
    public abstract void Visit(BattleFieldFigure visitor);
    /// <summary>
    /// Выполняется каждый ход
    /// </summary>
    public abstract void Execute();

    /// <summary>
    /// Позволяет разместить объект на переданную клетку
    /// </summary>
    /// <param name="cell"></param>
    public virtual void MoveToAnotherCell(BattleFieldCell cell)
    {
        if (Cell != null)
            Cell.BattleFieldObject = null;
        if (cell.BattleFieldObject != null)
        {
            cell.BattleFieldObject.DestroyThisBattleFieldObject();
        }
        cell.BattleFieldObject = this;
        Cell = cell;
        OnBoardPosition = Cell.OnBoardPosition;
        var cellPosition = Cell.RectTransform.localPosition;
        // Переносим и выравниваем
        Vector2 newPosition = new Vector2(cellPosition.x + Cell.RectTransform.rect.width / 2,
            cellPosition.y + Cell.RectTransform.rect.height / 2);
        GetComponent<RectTransform>().localPosition = newPosition;
        // Размещает объект по иерархии так, чтобы он отображался поверх клеток, но был ниже фигур
        transform.SetSiblingIndex(BattleField.Height * BattleField.Width + 2);
    }

    /// <summary>
    /// Уничтожает этот объект и обновляет информацию о себе у клетки
    /// </summary>
    public void DestroyThisBattleFieldObject()
    {
        if(_cell != null)
            _cell.BattleFieldObject = null;
        Destroy(gameObject);
    }

    /// <summary>
    /// При клике на объект выполняется клик клетки
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        _cell.OnPointerClick(eventData);
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }
}