using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWalk : Talent
{
    protected override void TalentAction()
    {
        BattleFieldCell cell;
        BattleFieldFigure enemy;
        if(_owner.Data.Team == _controller.BattleInfo.FirstFigure.Team)
        {
            enemy = _controller.BattleInfo.SecondFigure.BattleFieldFigureInstance;
            cell = GetMirrorCell(_controller.BattleInfo.FirstFigure.BattleFieldFigureInstance);
        }
        else
        {
            enemy = _controller.BattleInfo.FirstFigure.BattleFieldFigureInstance;
            cell = GetMirrorCell(_controller.BattleInfo.SecondFigure.BattleFieldFigureInstance);
        }

        if(cell.Figure == null)
        {
            if(cell.BattleFieldObject != null)
            {
                if(cell.BattleFieldObject.CanThisFigureToCross(enemy) != BarrierType.Impassable)
                {
                    StartCoroutine(enemy.MoveToAnotherCellWithAnimation(cell));
                }
            }
            else
                StartCoroutine(enemy.MoveToAnotherCellWithAnimation(cell));
        }
    }

    private BattleFieldCell GetMirrorCell(BattleFieldFigure figure)
    {
        Vector2Int currentFigurePosition = figure.OnBoardPosition;
        var mirrorCellVector = new Vector2Int(figure.BattleField.Width - 1 - currentFigurePosition.x,
                                              figure.BattleField.Height - 1 - currentFigurePosition.y);
        var mirrorCell = figure.BattleField.BattleFieldCells[mirrorCellVector.x, mirrorCellVector.y];
        return mirrorCell;
    }
}
