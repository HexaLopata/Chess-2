using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldHorse : MainFieldFigure
{
    public override Cell[] GetRelevantTurn(Cell[,] cells)
    {
        List<Cell> turns = new List<Cell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        if (cells.GetLength(1) > y + 2 && x > 0)
            turns.Add(cells[x - 1, y + 2]);
        if (cells.GetLength(1) > y + 2 && cells.GetLength(0) > x + 1)
            turns.Add(cells[x + 1, y + 2]);
        if (cells.GetLength(1) > y + 1 && cells.GetLength(0) > x + 2)
            turns.Add(cells[x + 2, y + 1]);
        if (cells.GetLength(1) > y + 1 && x - 1 > 0)
            turns.Add(cells[x - 2, y + 1]);
        if (y - 1 > 0 && x > 0)
            turns.Add(cells[x - 1, y - 2]);
        if (y - 1 > 0 && cells.GetLength(0) > x + 1)
            turns.Add(cells[x + 1, y - 2]);
        if (y > 0 && x - 1 > 0)
            turns.Add(cells[x - 2, y - 1]);
        if (y > 0 && cells.GetLength(0) > x + 2)
            turns.Add(cells[x + 2, y - 1]);
        return turns.ToArray();
    }
}
