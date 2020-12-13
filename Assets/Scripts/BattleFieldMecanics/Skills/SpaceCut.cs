using UnityEngine;

public class SpaceCut : Skill
{
    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var rawField = figure.BattleField.BattleFieldCells;

        bool canBeCutFromTheTop = true;
        bool canBeCutFromTheRight = true;

        if (figure.BattleField.BattleFieldCells.GetLength(0) <= 4)
            canBeCutFromTheRight = false;
        if (figure.BattleField.BattleFieldCells.GetLength(1) <= 4)
            canBeCutFromTheTop = false;

        for (int i = 0; i < rawField.GetLength(0); i++)
        {
            var currentCell = rawField[i, rawField.GetLength(1) - 1];
            if (currentCell.BattleFieldFigure != null)
                canBeCutFromTheTop = false;
        }

        for (int i = 0; i < rawField.GetLength(1); i++)
        {
            var currentCell = rawField[rawField.GetLength(0) - 1, rawField.GetLength(1) - 1 - i];
            if (currentCell.BattleFieldFigure != null)
                canBeCutFromTheRight = false;
        }

        if (canBeCutFromTheRight || canBeCutFromTheTop)
        {
            BattleFieldCell[,] newField = new BattleFieldCell[0, 0];

            if (canBeCutFromTheRight && canBeCutFromTheTop)
                newField = new BattleFieldCell[rawField.GetLength(0) - 1, rawField.GetLength(1) - 1];
            else if (canBeCutFromTheRight)
                newField = new BattleFieldCell[rawField.GetLength(0) - 1, rawField.GetLength(1)];
            else if (canBeCutFromTheTop)
                newField = new BattleFieldCell[rawField.GetLength(0), rawField.GetLength(1) - 1];

            for(int x = 0; x < rawField.GetLength(0); x++)
            {
                for(int y = 0; y < rawField.GetLength(1); y++)
                {
                    if (canBeCutFromTheRight && x >= newField.GetLength(0) || canBeCutFromTheTop && y >= newField.GetLength(1))
                        Destroy(rawField[x, y].gameObject);
                    else
                    {
                        newField[x, y] = rawField[x, y];
                        if (newField[x, y].BattleFieldObject != null)
                            newField[x, y].BattleFieldObject.DestroyThisBattleFieldObject();
                    }
                }
            }

            figure.BattleField.BattleFieldCells = newField;
        }
        figure.BattleField.BattleController.DeactivateAllCells();
        _controller.SwitchTurn();
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}