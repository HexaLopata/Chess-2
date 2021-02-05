using UnityEngine;
public class Berserker : Skill
{
    private int _baseDamage = -1;
    private int _baseDefence = -1;
    [SerializeField] private int _damageIfDefenceEqualsZero = 5;

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if(_baseDamage == -1)
            _baseDamage = figure.Damage;
        if(_baseDefence == -1)
            _baseDefence = figure.Defence;
            
        if(figure.Damage == _baseDamage)
        {
            if(figure.Defence == 0)
                figure.Damage += _damageIfDefenceEqualsZero;
            else
                figure.Damage += figure.Defence;
            figure.Defence = 0;
        }
        else
        {
            figure.Defence = _baseDefence;
            figure.Damage = _baseDamage;
        }
    }
}
