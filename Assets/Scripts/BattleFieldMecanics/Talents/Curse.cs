using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : Talent
{
    [SerializeField] private int _damage = 30;
    protected override void TalentAction()
    {
        if(_owner.Data.Team != _controller.BattleInfo.FirstFigure.Team)
        {
            _controller.BattleInfo.FirstFigure.BattleFieldFigureInstance.Health -= _damage;
        }
        else
            _controller.BattleInfo.SecondFigure.BattleFieldFigureInstance.Health -= _damage;
    }
}
