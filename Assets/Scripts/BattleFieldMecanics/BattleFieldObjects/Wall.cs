public class Wall : TimeLimitedObject
{
    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        if (figure.Data.Team != Team)
            return BarrierType.Impassable;
        return BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        if (figure.Data.Team != Team)
            return BarrierType.Stopable;
        return BarrierType.Passable;
    }

    public override void TakeDamage(BattleFieldFigure attacker)
    {
        if (attacker.Data.Team != Team)
            _turnRemains -= 0.5f;
    }

    public override void TakeDamage(int damage)
    {
        _turnRemains -= 0.5f;
    }

    public override void Visit(BattleFieldFigure visitor) { }
}
