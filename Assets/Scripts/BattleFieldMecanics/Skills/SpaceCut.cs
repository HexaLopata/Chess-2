public class SpaceCut : Skill
{
    public override string Name => "Space Cut";
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if(_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
        var rawField = figure.BattleField.BattleFieldCells;
        if (figure.BattleField.Width > 4 && figure.BattleField.Height > 4)
        {
            bool canBeCuted = true;
            for (int i = 0; i < rawField.GetLength(0); i++)
            {
                if (rawField[i, rawField.GetLength(1) - 1].BattleFieldFigure != null)
                    canBeCuted = false;
            }

            for (int i = 1; i < rawField.GetLength(1); i++)
            {
                if (rawField[rawField.GetLength(0) - 1, i].BattleFieldFigure != null)
                    canBeCuted = false;
            }

            if (canBeCuted)
            {
                for (int i = 0; i < rawField.GetLength(0); i++)
                {
                    Destroy(figure.BattleField.BattleFieldCells[i, rawField.GetLength(1) - 1].gameObject);
                    figure.BattleField.BattleFieldCells[i, rawField.GetLength(1) - 1] = null;
                }

                for (int i = rawField.GetLength(1) - 2; i >= 0; i--)
                {
                    Destroy(figure.BattleField.BattleFieldCells[rawField.GetLength(0) - 1, i].gameObject);
                    figure.BattleField.BattleFieldCells[rawField.GetLength(0) - 1, i] = null;
                }

                BattleFieldCell[,] newField = new BattleFieldCell[rawField.GetLength(0) - 1, rawField.GetLength(1) - 1];

                for (int i = 0; i < newField.GetLength(0); i++)
                {
                    for (int j = 0; j < newField.GetLength(1); j++)
                    {
                        newField[i, j] = rawField[i, j];
                    }
                }

                _controller.BattleField.BattleFieldCells = newField;
                _controller.SwitchTurn();
            }
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}