﻿public class MainFieldQueen : MainFieldFigure
{
    public override CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        _turns.Clear();
        GetEndlessDistanceMoves(1, 1);
        GetEndlessDistanceMoves(-1, 1);
        GetEndlessDistanceMoves(1, -1);
        GetEndlessDistanceMoves(-1, -1);
        GetEndlessDistanceMoves(0, 1);
        GetEndlessDistanceMoves(-1, 0);
        GetEndlessDistanceMoves(0, -1);
        GetEndlessDistanceMoves(1, 0);
        return _turns.ToArray();
    }
}