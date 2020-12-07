/// <summary>
/// Общий класс для фигур, ходящих на бесконечное расстояние
/// </summary>
public abstract class EndLessDistanceFigure : BattleFieldFigure
{
    /// <summary>
    /// Добавляет в список ходов все доступные клетки в заданном направлении
    /// </summary>
    /// <param name="cellX"></param>
    /// <param name="cellY"></param>
    /// <param name="isAttack"></param>
    protected void GetEndlessDistanceMoves(int cellX, int cellY, bool isAttack)
    {
        var localX = OnBoardPosition.x;
        var localY = OnBoardPosition.y;

        bool isCellExists = IsCellExists(cellX, cellY, localX, localY);

        while (isCellExists)
        {
            var x = localX + cellX;
            var y = localY + cellY;
            var currentCell = _battleFieldCells[x, y];

            if (currentCell != null)
            {

                if (currentCell.BattleFieldObject == null)
                {
                    _turns.Add(currentCell);
                }
                else
                {
                    BarrierType barrier;
                    if (isAttack)
                        barrier = currentCell.BattleFieldObject.CanThisFigureToAttackThrough(this);
                    else
                        barrier = currentCell.BattleFieldObject.CanThisFigureToCross(this);

                    if (barrier == BarrierType.Passable)
                    {
                        _turns.Add(currentCell);
                    }
                    else if (barrier == BarrierType.Stopable)
                    {
                        _turns.Add(currentCell);
                        break;
                    }
                    else
                        break;
                }

                if (currentCell.BattleFieldFigure != null)
                {
                    if (!isAttack)
                        _turns.Remove(currentCell);
                    break;
                }
            }

            localX += cellX;
            localY += cellY;

            isCellExists = IsCellExists(cellX, cellY, localX, localY);
        }
    }

    /// <summary>
    /// Вспомогательный метод для определения, существует ли клетка в заданном направлении
    /// </summary>
    /// <param name="cellX"></param>
    /// <param name="cellY"></param>
    /// <param name="localX"></param>
    /// <param name="localY"></param>
    /// <returns></returns>
    private bool IsCellExists(int cellX, int cellY, int localX, int localY)
    {
        bool result;
        if (cellX > 0)
            result = _battleFieldCells.GetLength(0) > localX + cellX;
        else if (cellX < 0)
            result = localX > 0;
        else
            result = true;
        if (cellY > 0)
            result = result && _battleFieldCells.GetLength(1) > localY + cellY;
        else if (cellY < 0)
            result = result && localY > 0;
        return result;
    }
}