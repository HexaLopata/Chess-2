using UnityEngine;

public class Shield : TimeLimitedObject
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
            return BarrierType.Impassable;
        return BarrierType.Passable;
    }

    public override void TakeDamage(BattleFieldFigure attacker) { }
    public override void TakeDamage(int damage) { }

    public override void Visit(BattleFieldFigure visitor) { }
}