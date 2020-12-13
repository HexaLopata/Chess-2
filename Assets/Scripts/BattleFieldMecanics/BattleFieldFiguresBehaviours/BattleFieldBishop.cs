public class BattleFieldBishop : EndLessDistanceFigure
{
    public override BattleFieldCell[] GetRelevantMoves()
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 1, false);
        GetEndlessDistanceMoves(-1, 1, false);
        GetEndlessDistanceMoves(1, -1, false);
        GetEndlessDistanceMoves(-1, -1, false);
        if(_battleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, -1, 0))
            CheckCellAndAdd(-1, 0, false);
        if (_battleField.IsCellExists(OnBoardPosition.x, OnBoardPosition.y, 1, 0))
            CheckCellAndAdd(1, 0, false);
        return _turns.ToArray();
    }

    public override BattleFieldCell[] GetRelevantAttackMoves()
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 1, true);
        GetEndlessDistanceMoves(-1, 1, true);
        GetEndlessDistanceMoves(1, -1, true);
        GetEndlessDistanceMoves(-1, -1, true);
        return _turns.ToArray();
    }
}