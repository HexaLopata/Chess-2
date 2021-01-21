using UnityEngine;

public class KingsKiller : Talent
{
    private int _baseOwnerDamage = -1; // -1 если еще не выставлено

    [SerializeField] private float _damageMultiply = 1.5f;

    protected override void TalentAction()
    {
        var enemy = _controller.BattleInfo.FirstFigure.BattleFieldFigureInstance;
        if(object.ReferenceEquals(enemy, _owner))
            enemy = _controller.BattleInfo.SecondFigure.BattleFieldFigureInstance;

        if (_baseOwnerDamage == -1)
            _baseOwnerDamage = _owner.Damage;
        if (enemy.FigureType == FigureType.King || enemy.FigureType == FigureType.Queen)
            _owner.Damage = (int)(_baseOwnerDamage * _damageMultiply);
    }
}

