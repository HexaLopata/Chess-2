public class MainFieldBishop : MainFieldFigure
{
    public override CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 1);
        GetEndlessDistanceMoves(-1, 1);
        GetEndlessDistanceMoves(1, -1);
        GetEndlessDistanceMoves(-1, -1);
        return _turns.ToArray();
    }
}