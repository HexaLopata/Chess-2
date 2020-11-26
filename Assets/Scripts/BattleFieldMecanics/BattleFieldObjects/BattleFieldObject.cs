using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
public abstract class BattleFieldObject : MonoBehaviour, IPointerClickHandler
{
    #region public Properties

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

    #endregion

    #region private Fields
    
    private BattleFieldCell _cell;
    private Team _team;
    private RectTransform _rectTransform;
    protected Image _image;

    [SerializeField] private Sprite _whiteSkin;
    [SerializeField] private Sprite _blackSkin;

    #endregion

    #region public Methods

    public abstract BarrierType CanThisFigureToCross(BattleFieldFigure figure);
    public abstract BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure);
    public abstract void TakeDamage(BattleFieldFigure attacker);
    public abstract void TakeDamage(int damage);
    public abstract void Visit(BattleFieldFigure visitor);
    public abstract void Execute();
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
    public void DestroyThisBattleFieldObject()
    {
        if(_cell != null)
            _cell.BattleFieldObject = null;
        Destroy(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _cell.OnPointerClick(eventData);
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }

    #endregion
}