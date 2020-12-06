using System.Collections.Generic;

public class MainFieldPawn : MainFieldFigure
{
    public bool IsFirstTurn { get; set; } = true;

    public override CellBase[] GetRelevantTurn(CellBase[,] cells)
    {
        List<CellBase> turns = new List<CellBase>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        if(Data.Team == Team.White)
        {
            if(cells.GetLength(1) > y)
            {
                if(cells[x, y + 1].Figure == null)
                {
                    turns.Add(cells[x, y + 1]);

                    if(cells.GetLength(1) > y + 1 && IsFirstTurn && cells[x, y + 2].Figure == null)
                        turns.Add(cells[x, y + 2]);
                }

                if(cells.GetLength(0) > x + 1 && cells[x + 1, y + 1].Figure != null)
                    turns.Add(cells[x + 1, y + 1]);

                if(x > 0 && cells[x - 1, y + 1].Figure != null)
                    turns.Add(cells[x - 1, y + 1]);
            }
        }
        else
        {
            if(y > 0)
            {
                if(cells[x, y - 1].Figure == null)
                {
                    turns.Add(cells[x, y - 1]);

                    if(y - 1 > 0 && IsFirstTurn && cells[x, y - 2].Figure == null)
                        turns.Add(cells[x, y - 2]);
                }

                if(cells.GetLength(0) > x + 1 && cells[x + 1, y - 1].Figure != null)
                    turns.Add(cells[x + 1, y - 1]);

                if(x > 0 && cells[x - 1, y - 1].Figure != null)
                    turns.Add(cells[x - 1, y - 1]);
            }
        }
        return turns.ToArray();
    }

    public override void MoveToAnotherCell(CellBase cellBase)
    {
        base.MoveToAnotherCell(cellBase);
        if(IsFirstTurn)
            IsFirstTurn = false;       
    }
}
