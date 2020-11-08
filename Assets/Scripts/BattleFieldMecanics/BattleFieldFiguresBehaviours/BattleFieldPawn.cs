using System.Collections.Generic;
using UnityEngine;

public class BattleFieldPawn : BattleFieldFigure
{
    public override FigureData Data
    {
        get
        {
            return _data;
        }
        set
        {
            if (value != null && value.Team == Team.White)
                Direction = true;
            else
                Direction = false;
            base.Data = value;
        } 
    }

    /// <summary>
    /// true - forward, false - back
    /// </summary>
    public bool Direction { get; set; } = true;

    public override BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY, BattleFieldCell[,] cells)
        {
            if (cells[cellX, cellY ].BattleFieldObject == null ||
                cells[cellX, cellY].BattleFieldObject.IsItPossibleToCross == BarrierType.Impassable) ; 
            {
                if(cells[cellX, cellY].BattleFieldFigure == null)
                    turnsList.Add(cells[cellX, cellY]);
            }
        }
        
        if (Direction)
        {
            if (battleFieldCells.GetLength(1) > y + 1 && battleFieldCells.GetLength(0) > x + 1)
            {
                CheckAllCellsAndAdd(turns, x + 1, y + 1, battleFieldCells);
            }

            if (battleFieldCells.GetLength(1) > y + 1 && x > 0)
            {
                CheckAllCellsAndAdd(turns, x - 1, y + 1, battleFieldCells);
            }
        }
        else
        {
            if (y > 0 && battleFieldCells.GetLength(0) > x + 1)
            {
                CheckAllCellsAndAdd(turns, x + 1, y - 1, battleFieldCells);
            }

            if (y > 0 && x > 0)
            {
                CheckAllCellsAndAdd(turns, x - 1, y - 1, battleFieldCells);
            }
        }

        return turns.ToArray();
    }
    
    public override BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY, BattleFieldCell[,] cells)
        {
            if (cells[cellX, cellY].BattleFieldObject == null ||
                cells[cellX, cellY].BattleFieldObject.IssItPossibleToAttackThrough == BarrierType.Impassable) ; 
            {
                turnsList.Add(cells[cellX, cellY]);
            }
        }
        
        if (battleFieldCells.GetLength(1) > y + 1 && battleFieldCells.GetLength(0) > x + 1)
        {
            CheckAllCellsAndAdd(turns, x + 1, y + 1, battleFieldCells);
        }

        if (battleFieldCells.GetLength(1) > y + 1 && x > 0)
        {
            CheckAllCellsAndAdd(turns, x - 1, y + 1, battleFieldCells);
        }
        
        if (y > 0 && battleFieldCells.GetLength(0) > x + 1)
        {
            CheckAllCellsAndAdd(turns, x + 1, y - 1, battleFieldCells);
        }

        if (y > 0 && x > 0)
        {
            CheckAllCellsAndAdd(turns, x - 1, y - 1, battleFieldCells);
        }

        return turns.ToArray();
    }

    public override void MoveToAnotherCell(Cell cell)
    {
        base.MoveToAnotherCell(cell);
        if (_battleField != null)
        {
            if (Direction && _battleField.Height - 1 == OnBoardPosition.y)
                Direction = false;
            else if (!Direction && OnBoardPosition.y == 0)
                Direction = true;
        }
    }

    protected override void SetDamageAndDefence()
    {
        Damage = 40;
        Defence = 0;
    }
}