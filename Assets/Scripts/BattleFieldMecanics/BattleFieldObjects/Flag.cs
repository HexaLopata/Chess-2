public class Flag : TimeLimitedObject
{
    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return figure.Data.Team != Team ? BarrierType.Impassable : BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override void TakeDamage(BattleFieldFigure attacker) { }

    public override void TakeDamage(int damage) { }

    public override void Visit(BattleFieldFigure visitor) { }
}