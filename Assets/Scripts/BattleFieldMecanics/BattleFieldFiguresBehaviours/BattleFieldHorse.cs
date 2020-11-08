using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldHorse : BattleFieldFigure
{
    public override BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        List<BattleFieldCell> turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(int cellX, int cellY)
        {
            if (battleFieldCells[x + cellX, y + cellY].BattleFieldFigure != null)
            {
                if (battleFieldCells[x + cellX, y + cellY].BattleFieldObject == null)
                {
                    turns.Add(battleFieldCells[x + cellX, y + cellY]);
                }
                else
                {
                    if (battleFieldCells[x + cellX, y + cellY].BattleFieldObject.IssItPossibleToAttackThrough !=
                        BarrierType.Impassable) ;
                    turns.Add(battleFieldCells[x + cellX, y + cellY]);
                }
            }
        }

        if (battleFieldCells.GetLength(1) > y + 2 && x > 0)
            CheckAllCellsAndAdd(-1, 2);
        if (battleFieldCells.GetLength(1) > y + 2 && battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, 2);
        if (battleFieldCells.GetLength(1) > y + 1 && battleFieldCells.GetLength(0) > x + 2)
            CheckAllCellsAndAdd(2, 1);
        if (battleFieldCells.GetLength(1) > y + 1 && x - 1 > 0)
            CheckAllCellsAndAdd(-2, 1);
        if (y - 1 > 0 && x > 0)
            CheckAllCellsAndAdd(-1, -2);
        if (y - 1 > 0 && battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, -2);
        if (y > 0 && x - 1 > 0)
            CheckAllCellsAndAdd(-2, -1);
        if (y > 0 && battleFieldCells.GetLength(0) > x + 2)
            CheckAllCellsAndAdd(2, -1);
        return turns.ToArray();
    }

    public override BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells)
    {
        List<BattleFieldCell> turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;
        
        void CheckAllCellsAndAdd(int cellX, int cellY)
        {
            if (battleFieldCells[x + cellX, y + cellY].BattleFieldObject == null)
            {
                turns.Add(battleFieldCells[x + cellX, y + cellY]);
            }
            else
            {
                if (battleFieldCells[x + cellX, y + cellY].BattleFieldObject.IssItPossibleToAttackThrough !=
                    BarrierType.Impassable) ;
                turns.Add(battleFieldCells[x + cellX, y + cellY]);
            }
        }

        if (battleFieldCells.GetLength(1) > y + 2 && x > 0)
            CheckAllCellsAndAdd(-1, 2);
        if (battleFieldCells.GetLength(1) > y + 2 && battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, 2);
        if (battleFieldCells.GetLength(1) > y + 1 && battleFieldCells.GetLength(0) > x + 2)
            CheckAllCellsAndAdd(2, 1);
        if (battleFieldCells.GetLength(1) > y + 1 && x - 1 > 0)
            CheckAllCellsAndAdd(-2, 1);
        if (y - 1 > 0 && x > 0)
            CheckAllCellsAndAdd(-1, -2);
        if (y - 1 > 0 && battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, -2);
        if (y > 0 && x - 1 > 0)
            CheckAllCellsAndAdd(-2, -1);
        if (y > 0 && battleFieldCells.GetLength(0) > x + 2)
            CheckAllCellsAndAdd(2, -1);
        return turns.ToArray();
    }
    protected override void SetDamageAndDefence()
    {
        Damage = 40;
        Defence = 10;
    }
}
