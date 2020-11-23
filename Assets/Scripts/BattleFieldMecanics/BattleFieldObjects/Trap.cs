public class Trap : BattleFieldObject
{
    private int _damage = 35;
    private float _turnRemains = 10;
    public override BarrierType CanThisFigureToCross(BattleFieldFigure figure)
    {
        if (figure.Data.Team != Team)
        {
            return BarrierType.Stopable;
        }
        else
        {
            return BarrierType.Passable;
        }
    }

    public override BarrierType CanThisFigureToAttackThrough(BattleFieldFigure figure)
    {
        if (figure.Data.Team != Team)
        {
            return BarrierType.Passable;
        }
        else
        {
            return BarrierType.Passable;
        }
    }

    public override void TakeDamage(BattleFieldFigure attacker)
    {
        if (attacker.Data.Team != Team)
        {
            attacker.TakeDamage(_damage);
            DestroyThisBattleFieldObject();
        }
    }

    public override void Visit(BattleFieldFigure visitor)
    {
        if (visitor.Data.Team != Team)
        {
            visitor.TakeDamage(_damage);
            DestroyThisBattleFieldObject();
        }
    }

    public override void Execute()
    {
        _turnRemains -= 0.5f;
        if (_turnRemains <= 0)
        {
            DestroyThisBattleFieldObject();
        }
    }
}
