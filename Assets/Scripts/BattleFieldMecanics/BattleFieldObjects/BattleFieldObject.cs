using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(RectTransform))]
public abstract class BattleFieldObject : MonoBehaviour
{
    public BarrierType IsItPossibleToCross => _isItPossibleToCross;
    public BarrierType IssItPossibleToAttackThrough => _isItPossibleToAttackThrough;
    public Vector2Int OnBoardPosition;
    public BattleFieldCell Cell { get; set; }

    [SerializeField] private BarrierType _isItPossibleToAttackThrough;
    [SerializeField] private BarrierType _isItPossibleToCross;

    public virtual void Visit(BattleFieldFigure visitor) { }
    
    public virtual void MoveToAnotherCell(BattleFieldCell cell)
    {
        if (Cell != null)
            Cell.BattleFieldObject = null;
        cell.BattleFieldObject = this;
        Cell = cell;
        OnBoardPosition = Cell.OnBoardPosition;
        var cellPosition = Cell.RectTransform.localPosition;
        // Переносим и выравниваем
        Vector2 newPosition = new Vector2(cellPosition.x + Cell.RectTransform.rect.width / 2,
            cellPosition.y + Cell.RectTransform.rect.height / 2);
        GetComponent<RectTransform>().localPosition = newPosition;
    }
}