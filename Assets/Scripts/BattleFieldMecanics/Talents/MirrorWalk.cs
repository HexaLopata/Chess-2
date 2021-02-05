using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorWalk : Talent
{
    protected override void TalentAction()
    {
        BattleFieldCell cell;
        if(_owner.Data.Team != _controller.BattleInfo.FirstFigure.Team)
        {
            cell = GetMirrorCell(_controller.BattleInfo.FirstFigure.BattleFieldFigureInstance);
        }
        else
            cell = GetMirrorCell(_controller.BattleInfo.SecondFigure.BattleFieldFigureInstance);

        if(cell.Figure == null)
        {
            if(cell.BattleFieldObject != null)
            {
                if(cell.BattleFieldObject.CanThisFigureToCross(_owner) != BarrierType.Impassable)
                {
                    StartCoroutine(_owner.MoveToAnotherCellWithAnimation(cell));
                }
            }
            else
                StartCoroutine(_owner.MoveToAnotherCellWithAnimation(cell));
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
