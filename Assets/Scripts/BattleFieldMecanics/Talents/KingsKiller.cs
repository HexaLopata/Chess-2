using UnityEngine;

public class KingsKiller : Talent
{
    private int _baseOwnerDamage = -1; // -1 если еще не выставлено

    [SerializeField] private float _damageMultiply = 1.5f;

    protected override void TalentAction()
    {
        if (_baseOwnerDamage == -1)
            _baseOwnerDamage = _owner.Damage;
        if (_controller.CurrentFigure.Data.MainFieldFigurePrefub.King)
            _owner.Damage = (int)(_baseOwnerDamage * _damageMultiply);
    }
}

