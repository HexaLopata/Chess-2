public class BattleFieldRook : EndLessDistanceFigure
{
    public override BattleFieldCell[] GetRelevantMoves()
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 0, false);
        GetEndlessDistanceMoves(0, 1, false);
        GetEndlessDistanceMoves(0, -1, false);
        GetEndlessDistanceMoves(-1, 0, false);
        return _turns.ToArray();
    }

    public override BattleFieldCell[] GetRelevantAttackMoves()
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 0, true);
        GetEndlessDistanceMoves(0, 1, true);
        GetEndlessDistanceMoves(0, -1, true);
        GetEndlessDistanceMoves(-1, 0, true);
        return _turns.ToArray();
    }
}