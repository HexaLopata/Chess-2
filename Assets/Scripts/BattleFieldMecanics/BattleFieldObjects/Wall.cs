public class Wall : BattleFieldObject
{ 
    private float _turnRemains = 6;
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
        _turnRemains -= 0.5f;
    }

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
