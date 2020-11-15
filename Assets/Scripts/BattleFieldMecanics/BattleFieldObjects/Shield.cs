public class Shield : BattleFieldObject
{
    private float _turnRemains = 5;
    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        return BarrierType.Passable;
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        if (figure.Data.Team != Team)
            return BarrierType.Impassable;
        return BarrierType.Passable;
    }

    public override void TakeDamage(BattleFieldFigure attacker) { }

    public override void Visit(BattleFieldFigure visitor) { }

    public override void Execute()
    {
        _turnRemains -= 0.5f;
        if (_turnRemains <= 0)
        {
            DestroyThisBattleFieldObject();
        }
    }
}