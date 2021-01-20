using System.Collections;
using UnityEngine;

public class CounterAttack : Skill
{
    private Coroutine _attack;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if(_attack == null)
            _attack =  StartCoroutine(DoubleAttack(figure));
    }

    private IEnumerator DoubleAttack(BattleFieldFigure figure)
    {
        while(_controller != null && _controller.IsAnimationPlaying)
            yield return new WaitForSeconds(BattleController.animationDelay); 
        _controller.IsAnimationPlaying = true;
        figure.LaunchAnAttack();
        yield return new WaitForSeconds(0.5f);
        figure.LaunchAnAttack();
        _controller.IsAnimationPlaying = false;
        _controller.SwitchTurn();
        _attack = null;
    }
    
    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
