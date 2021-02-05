using UnityEngine;

public class Heal : Skill
{
    [SerializeField] int _healAmount = 5;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        figure.Health += _healAmount;
        _controller.SwitchTurn();
    }
    
    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
