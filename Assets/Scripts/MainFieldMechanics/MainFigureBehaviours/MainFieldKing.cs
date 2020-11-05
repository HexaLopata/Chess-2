using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldKing : MainFieldFigure
{
    public override Cell[] GetRelevantTurn(Cell[,] cells)
    {
        List<Cell> turns = new List<Cell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        if (cells.GetLength(0) > x + 1)
            turns.Add(cells[x + 1, y]);
        if (cells.GetLength(1) > y + 1) 
            turns.Add(cells[x, y + 1]);
        if (y > 0)
            turns.Add(cells[x, y - 1]);
        if (x > 0)
            turns.Add(cells[x - 1, y]);
        if (cells.GetLength(0) > x + 1 && cells.GetLength(1) > y + 1)
            turns.Add(cells[x + 1, y + 1]);
        if (x > 0 && cells.GetLength(1) > y + 1) 
            turns.Add(cells[x - 1, y + 1]);
        if (cells.GetLength(0) > x + 1 && y > 0)
            turns.Add(cells[x + 1, y - 1]);
        if (x > 0 && y > 0)
            turns.Add(cells[x - 1, y - 1]);
        return turns.ToArray();
    }
}
