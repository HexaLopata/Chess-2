public class Sacrifice : Skill
{
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_delay <= 0)
        {
            var rawField = figure.BattleField.BattleFieldCells;
            for (int x = 0; x < rawField.GetLength(0); x++)
            {
                for (int y = 0; y < rawField.GetLength(1); y++)
                {
                    rawField[x, y].TakeDamage(figure);
                }
            }

            _delay = _maxDelay;
            figure.TakeDamage(1000);
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
