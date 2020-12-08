using UnityEngine;

public class ShadowCloning : Skill
{
    [SerializeField] private ShadowClone _shadowClone;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var shadowClone = Instantiate(_shadowClone, figure.BattleField.transform);
        shadowClone.Team = figure.Data.Team;
        shadowClone.Owner = figure;
        _controller.SwitchTurn();
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
