using System.Collections;
using UnityEngine;

public class CounterAttack : Skill
{
    public override string Name  => "Feint";
    private Coroutine _attack;
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
        if (_delay <= 0)
        {
            _delay = _maxDelay;
            if(_attack == null)
                _attack =  StartCoroutine(DoubleAttack(figure));
        }
    }

    private IEnumerator DoubleAttack(BattleFieldFigure figure)
    {
        figure.LaunchAnAttack();
        yield return new WaitForSeconds(0.5f);
        figure.LaunchAnAttack();
        _controller.SwitchTurn();
        _attack = null;
    }
    
    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
