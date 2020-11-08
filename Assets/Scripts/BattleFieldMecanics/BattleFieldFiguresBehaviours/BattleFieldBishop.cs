﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldBishop : BattleFieldFigure
{
    public override BattleFieldCell[] GetRelevantMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();

        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY)
        {
            var localX = OnBoardPosition.x;
            var localY = OnBoardPosition.y;
            bool condition;
            
            if (cellX > 0)
                condition = battleFieldCells.GetLength(0) > localX + cellX;
            else
                condition = localX > 0;
            if (cellY > 0)
                condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
            else
                condition = condition && localY > 0;

            while (condition)
            {
                if (battleFieldCells[localX + cellX, localY + cellY].Figure == null)
                {
                    if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject == null)
                    {
                        turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                    }
                    else
                    {
                        if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject.IsItPossibleToCross == BarrierType.Passable)
                            turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                        else if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject.IsItPossibleToCross ==
                                 BarrierType.Stopable)
                        {
                            turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                            break;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    break;
                }

                localX += cellX;
                localY += cellY;
                
                if (cellX > 0)
                    condition = battleFieldCells.GetLength(0) > localX + cellX;
                else
                    condition = localX > 0;
                if (cellY > 0)
                    condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
                else
                    condition = condition && localY > 0;
            }
        }
        
        CheckAllCellsAndAdd(turns, 1, 1);
        CheckAllCellsAndAdd(turns, -1, 1);
        CheckAllCellsAndAdd(turns, 1, -1);
        CheckAllCellsAndAdd(turns, -1, -1);
        return turns.ToArray();
    }

    public override BattleFieldCell[] GetRelevantAttackMoves(BattleFieldCell[,] battleFieldCells)
    {
        var turns = new List<BattleFieldCell>();

        void CheckAllCellsAndAdd(List<BattleFieldCell> turnsList, int cellX, int cellY)
        {
            var localX = OnBoardPosition.x;
            var localY = OnBoardPosition.y;
            bool condition;

            if (cellX > 0)
                condition = battleFieldCells.GetLength(0) > localX + cellX;
            else
                condition = localX > 0;
            if (cellY > 0)
                condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
            else
                condition = condition && localY > 0;

            while (condition)
            {
                if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject == null)
                {
                    turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                }
                else
                {
                    if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                        .IssItPossibleToAttackThrough == BarrierType.Passable)
                        turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                    else if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                                 .IssItPossibleToAttackThrough ==
                             BarrierType.Stopable)
                    {
                        turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                        break;
                    }
                    else
                        break;
                }

                localX += cellX;
                localY += cellY;

                if (cellX > 0)
                    condition = battleFieldCells.GetLength(0) > localX + cellX;
                else
                    condition = localX > 0;
                if (cellY > 0)
                    condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
                else
                    condition = condition && localY > 0;
            }
        }

        CheckAllCellsAndAdd(turns, 1, 1);
        CheckAllCellsAndAdd(turns, -1, 1);
        CheckAllCellsAndAdd(turns, 1, -1);
        CheckAllCellsAndAdd(turns, -1, -1);
        return turns.ToArray();
    }
    protected override void SetDamageAndDefence()
    {      
        Damage = 30;
        Defence = 15;
    }
}