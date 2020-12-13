using System.Collections;

public class BattleFieldPawn : BattleFieldFigure
{
    private bool _isMovingForward = false;

    public override BattleFieldCell[] GetRelevantMoves()
    {
        return ReturnAllRelevantTurns(false);
    }
    
    public override BattleFieldCell[] GetRelevantAttackMoves()
    {
        return ReturnAllRelevantTurns(true);
    }

    private BattleFieldCell[] ReturnAllRelevantTurns(bool isAttack)
    {
        _turns.Clear();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;

        if (_battleFieldCells.GetLength(1) > y + 1 && _battleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, 1, isAttack);
        if (_battleFieldCells.GetLength(1) > y + 1 && x > 0)
            CheckCellAndAdd(-1, 1, isAttack);
        if (y > 0 && _battleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, -1, isAttack);
        if (y > 0 && x > 0)
            CheckCellAndAdd(-1, -1, isAttack);
        if (!isAttack)
        {
            if (!_isMovingForward)
            {
                if (_battleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, -1))
                    CheckCellAndAdd(0, -1, false);
            }
            else
            {
                if (_battleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 0, 1))
                    CheckCellAndAdd(0, 1, false);
            }
        }

        return _turns.ToArray();
    }

    public override IEnumerator MoveToAnotherCellWithAnimation(CellBase cellBase)
    {
        var moveAnimation =  base.MoveToAnotherCellWithAnimation(cellBase);
        if (cellBase.OnBoardPosition.y == 0)
            _isMovingForward = true;
        if(BattleField != null && cellBase.OnBoardPosition.y == BattleField.Height - 1)
            _isMovingForward = false;
        return moveAnimation;
    }
}