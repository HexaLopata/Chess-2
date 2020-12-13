using UnityEngine;

public class Bloodlust : Talent
{
    private int _baseOwnerDamage = -1; // -1 если еще не выставлено

    [SerializeField] private float _multiply = 1;

    protected override void TalentAction()
    {
        if (_baseOwnerDamage == -1)
            _baseOwnerDamage = _owner.Damage;
        _owner.Damage = (int)(_baseOwnerDamage + _baseOwnerDamage * ((BattleFieldFigure.maxHealth - _owner.Health) * 0.01f) * _multiply);
    }
}
