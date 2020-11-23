public class Sacrifice : Skill
{
    public override string Name => "Sacrifice";
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var rawField = figure.BattleField.BattleFieldCells;
        for (int x = 0; x  < rawField.GetLength(0); x++)
        {
            for (int y = 0; y < rawField.GetLength(1); y++)
            {
                rawField[x, y].TakeDamage(figure);
            }
        }
        figure.TakeDamage(1000);
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
