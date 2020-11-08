using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldKing : BattleFieldFigure
{
    public override BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
        var x = OnBoardPosition.x;
        var y = OnBoardPosition.y;

        void CheckAllCellsAndAdd(int cellX, int cellY)
        {
            if (battleFieldCells[x + cellX, y + cellY].BattleFieldFigure == null)
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
        if (battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, 0);
        if (battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(0, 1);
        if (y > 0)
            CheckAllCellsAndAdd(0, -1);
        if (x > 0)
            CheckAllCellsAndAdd(-1, 0);
        if (battleFieldCells.GetLength(0) > x + 1 && battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(1, 1);
        if (x > 0 && battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(-1, 1);
        if (battleFieldCells.GetLength(0) > x + 1 && y > 0)
            CheckAllCellsAndAdd(1, -1);
        if (x > 0 && y > 0)
            CheckAllCellsAndAdd(-1, -1);
        return turns.ToArray();
    }

    public override BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();
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
        if (battleFieldCells.GetLength(0) > x + 1)
            CheckAllCellsAndAdd(1, 0);
        if (battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(0, 1);
        if (y > 0)
            CheckAllCellsAndAdd(0, -1);
        if (x > 0)
            CheckAllCellsAndAdd(-1, 0);
        if (battleFieldCells.GetLength(0) > x + 1 && battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(1, 1);
        if (x > 0 && battleFieldCells.GetLength(1) > y + 1)
            CheckAllCellsAndAdd(-1, 1);
        if (battleFieldCells.GetLength(0) > x + 1 && y > 0)
            CheckAllCellsAndAdd(1, -1);
        if (x > 0 && y > 0)
            CheckAllCellsAndAdd(-1, -1);
        return turns.ToArray();
    }
    protected override void SetDamageAndDefence()
    {
        Damage = 30;
        Defence = 5;
    }
}