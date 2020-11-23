using System.Collections.Generic;

public class BattleFieldQueen : BattleFieldFigure
{
    #region public Methods

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
            else if (cellX < 0)
                condition = localX > 0;
            else
                condition = true;
            if (cellY > 0)
                condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
            else if (cellY < 0)
                condition = condition && localY > 0;

            while (condition)
            {
                if (battleFieldCells[localX + cellX, localY + cellY] != null)
                {
                    if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldFigure == null)
                    {
                        if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject == null)
                        {
                            turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                        }
                        else
                        {
                            if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                                .CanThisFigureToCross(this) == BarrierType.Passable)
                                turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                            else if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                                         .CanThisFigureToCross(this) ==
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
                        break;
                }

                localX += cellX;
                localY += cellY;

                if (cellX > 0)
                    condition = battleFieldCells.GetLength(0) > localX + cellX;
                else if (cellX < 0)
                    condition = localX > 0;
                else
                    condition = true;
                if (cellY > 0)
                    condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
                else if (cellY < 0)
                    condition = condition && localY > 0;
            }
        }
        
        CheckAllCellsAndAdd(turns, 1, 1);
        CheckAllCellsAndAdd(turns, -1, 1);
        CheckAllCellsAndAdd(turns, 1, -1);
        CheckAllCellsAndAdd(turns, -1, -1);
        CheckAllCellsAndAdd(turns, 1, 0);
        CheckAllCellsAndAdd(turns, 0, 1);
        CheckAllCellsAndAdd(turns, 0, -1);
        CheckAllCellsAndAdd(turns, -1, 0);
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
            else if (cellX < 0)
                condition = localX > 0;
            else
                condition = true;
            if (cellY > 0)
                condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
            else if (cellY < 0)
                condition = condition && localY > 0;

            while (condition)
            {
                if (battleFieldCells[localX + cellX, localY + cellY] != null)
                {
                    if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject == null)
                    {
                        turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                    }
                    else
                    {
                        if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                            .CanThisFigureToAttackThrough(this) == BarrierType.Passable)
                            turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                        else if (battleFieldCells[localX + cellX, localY + cellY].BattleFieldObject
                                     .CanThisFigureToAttackThrough(this) ==
                                 BarrierType.Stopable)
                        {
                            turns.Add(battleFieldCells[localX + cellX, localY + cellY]);
                            break;
                        }
                        else
                            break;
                    }
                }

                localX += cellX;
                localY += cellY;

                if (cellX > 0)
                    condition = battleFieldCells.GetLength(0) > localX + cellX;
                else if (cellX < 0)
                    condition = localX > 0;
                else
                    condition = true;
                if (cellY > 0)
                    condition = condition && battleFieldCells.GetLength(1) > localY + cellY;
                else if (cellY < 0)
                    condition = condition && localY > 0;
            }
        }
        
        CheckAllCellsAndAdd(turns, 1, 1);
        CheckAllCellsAndAdd(turns, -1, 1);
        CheckAllCellsAndAdd(turns, 1, -1);
        CheckAllCellsAndAdd(turns, -1, -1);
        CheckAllCellsAndAdd(turns, 1, 0);
        CheckAllCellsAndAdd(turns, 0, 1);
        CheckAllCellsAndAdd(turns, 0, -1);
        CheckAllCellsAndAdd(turns, -1, 0);
        return turns.ToArray();
    }

    #endregion

    #region protected Methods

    protected override void SetDamageAndDefence()
    {
        Damage = 30;
        Defence = 5;
    }

    #endregion
}
