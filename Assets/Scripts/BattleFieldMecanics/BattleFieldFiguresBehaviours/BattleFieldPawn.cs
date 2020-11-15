using System.Collections.Generic;

public class BattleFieldPawn : BattleFieldFigure
{
    #region public Methods

    public override BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY, BattleFieldCell[,] cells)
        {
            if (cells[cellX, cellY ].BattleFieldObject == null ||
                cells[cellX, cellY].BattleFieldObject.CanThisFigureToCross(this) != BarrierType.Impassable)
            {
                if(cells[cellX, cellY].BattleFieldFigure == null)
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
    
    public override BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY, BattleFieldCell[,] cells)
        {
            if (cells[cellX, cellY].BattleFieldObject == null ||
                cells[cellX, cellY].BattleFieldObject.CanThisFigureToAttackThrough(this) != BarrierType.Impassable)
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

    #endregion

    #region protected Methods

    protected override void SetDamageAndDefence()
    {
        Damage = 40;
        Defence = 0;
    }

    #endregion
}