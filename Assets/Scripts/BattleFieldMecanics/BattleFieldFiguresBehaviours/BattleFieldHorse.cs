public class BattleFieldHorse : BattleFieldFigure
{
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

        if (_battleFieldCells.GetLength(1) > y + 2 && x > 0)
            CheckCellAndAdd(-1, 2, isAttack);
        if (_battleFieldCells.GetLength(1) > y + 2 && _battleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, 2, isAttack);
        if (_battleFieldCells.GetLength(1) > y + 1 && _battleFieldCells.GetLength(0) > x + 2)
            CheckCellAndAdd(2, 1, isAttack);
        if (_battleFieldCells.GetLength(1) > y + 1 && x - 1 > 0)
            CheckCellAndAdd(-2, 1, isAttack);
        if (y - 1 > 0 && x > 0)
            CheckCellAndAdd(-1, -2, isAttack);
        if (y - 1 > 0 && _battleFieldCells.GetLength(0) > x + 1)
            CheckCellAndAdd(1, -2, isAttack);
        if (y > 0 && x - 1 > 0)
            CheckCellAndAdd(-2, -1, isAttack);
        if (y > 0 && _battleFieldCells.GetLength(0) > x + 2)
            CheckCellAndAdd(2, -1, isAttack);
        return _turns.ToArray();

    }
}