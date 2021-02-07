using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShadowClone : TimeLimitedObject
{
    public BattleFieldFigure Owner
    {
        get => _owner;
        set
        {
            _previousOwnerPosition = value.OnBoardPosition;
            _image.sprite = value.GetComponent<Image>().sprite;
            _image.color = Color.magenta;
            _owner = value;
            MoveToAnotherCell(GetMirrorCell());
        }
    }

    private BattleFieldFigure _owner;
    private Vector2Int _previousOwnerPosition;

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return BarrierType.Impassable;
    }

    public override void Execute()
    {
        base.Execute();
        if (_controller.CurrentTurn != _owner.Data.Team)
        {
            if (_turnRemains > 0)
            {
                StartCoroutine(Turn());
            }
        }
    }

    private IEnumerator Turn()
    {
        var newCell = GetMirrorCell();
        yield return MoveToAnotherCellWithAnimation(newCell);
        var attackTurns = _owner.GetRelevantAttackMovesFromCustomPosition(OnBoardPosition);
        foreach (var cell in attackTurns)
            cell.TakeDamage(_owner);
    }

    private BattleFieldCell GetMirrorCell()
    {
        Vector2Int currentOwnerPosition = _owner.OnBoardPosition;
        var mirrorCellVector = new Vector2Int(_owner.BattleField.Width - 1 - currentOwnerPosition.x,
                                              _owner.BattleField.Height - 1 - currentOwnerPosition.y);
        var mirrorCell = _owner.BattleField.BattleFieldCells[mirrorCellVector.x, mirrorCellVector.y];
        return mirrorCell;
    }

    public override void MoveToAnotherCell(BattleFieldCell cell)
    {
        base.MoveToAnotherCell(cell);
        if (cell.BattleFieldFigure != null)
            DestroyThisBattleFieldObject();
    }

    public override void TakeDamage(BattleFieldFigure attacker) { }

    public override void TakeDamage(int damage) { }

    public override void Visit(BattleFieldFigure visitor) { }
}
