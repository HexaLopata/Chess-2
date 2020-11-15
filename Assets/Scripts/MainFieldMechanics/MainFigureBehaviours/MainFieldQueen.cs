using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldQueen : MainFieldFigure
{
    public override CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        List<CellBase> turns = new List<CellBase>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        while (cells.GetLength(0) > x + 1)
        {
            turns.Add(cells[x + 1, y]);
            if(cells[x + 1, y].Figure != null)
                break;
            x++;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (cells.GetLength(1) > y + 1) 
        {
            turns.Add(cells[x, y + 1]);
            if(cells[x, y + 1].Figure != null)
                break;
            y++;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (y > 0)
        {
            turns.Add(cells[x, y - 1]);
            if(cells[x, y - 1].Figure != null)
                break;
            y--;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (x > 0)
        {
            turns.Add(cells[x - 1, y]);
            if(cells[x - 1, y].Figure != null)
                break;
            x--;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (cells.GetLength(0) > x + 1 && cells.GetLength(1) > y + 1)
        {
            turns.Add(cells[x + 1, y + 1]);
            if(cells[x + 1, y + 1].Figure != null)
                break;
            x++;
            y++;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (x > 0 && cells.GetLength(1) > y + 1) 
        {
            turns.Add(cells[x - 1, y + 1]);
            if(cells[x - 1, y + 1].Figure != null)
                break;
            x--;
            y++;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (cells.GetLength(0) > x + 1 && y > 0)
        {
            turns.Add(cells[x + 1, y - 1]);
            if(cells[x + 1, y - 1].Figure != null)
                break;
            x++;
            y--;
        }
        x = OnBoardPosition.x;
        y = OnBoardPosition.y;
        while (x > 0 && y > 0)
        {
            turns.Add(cells[x - 1, y - 1]);
            if(cells[x - 1, y - 1].Figure != null)
                break;
            x--;
            y--;
        }
        return turns.ToArray();
    }
}