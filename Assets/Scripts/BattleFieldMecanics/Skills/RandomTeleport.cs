using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTeleport : Skill
{
    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var figurePos = figure.OnBoardPosition;
        List<BattleFieldCell> turns = new List<BattleFieldCell>();
        var isEvenCell = (figurePos.x % 2 == 0) == (figurePos.y % 2 == 0);
        var battleFieldCells = figure.BattleField.BattleFieldCells;
        for (int x = 0; x < battleFieldCells.GetLongLength(0); x++)
        {
            for (int y = 0; y < battleFieldCells.GetLongLength(1); y++)
            {
                if ((battleFieldCells[x, y].OnBoardPosition.x % 2 == 0) ==
                    (battleFieldCells[x, y].OnBoardPosition.y % 2 == 0) == isEvenCell)
                {
                    if (battleFieldCells[x, y].BattleFieldFigure == null)
                    {
                        if (battleFieldCells[x, y].BattleFieldObject == null || battleFieldCells[x, y].BattleFieldObject.CanThisFigureToCross(figure) != BarrierType.Impassable)
                            turns.Add(battleFieldCells[x, y]);
                    }
                }
            }
        }

        var randomCell = turns[Random.Range(0, turns.Count)];
        StartCoroutine(MoveAndUpdateTurns(figure, randomCell));
    }

    private IEnumerator MoveAndUpdateTurns(BattleFieldFigure figure, BattleFieldCell cell)
    {
        figure.BattleField.BattleController.DeactivateAllCells();
        yield return figure.MoveToAnotherCellWithAnimation(cell);
        if (figure.GetRelevantMoves().Length > 0)
            figure.BattleField.BattleController.ActivateAllCells(figure.GetRelevantMoves());
        else
            figure.BattleField.BattleController.SwitchTurn();
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
