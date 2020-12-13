using System.Linq;
using UnityEngine;

public class Revenge : Talent
{
    private MainField _mainField;
    private int _baseOwnerDamage = -1;

    [SerializeField] private float _multiplyPerOneDeadAlly = 0.07f;

    protected override void TalentAction()
    {
        if (_baseOwnerDamage == -1)
            _baseOwnerDamage = _owner.Damage;
        if (_mainField == null)
            _mainField = _owner.Data.MainFieldFigureInstance.Field;

        int alliesCount = 0;
        if (_team == Team.Black)
            alliesCount = _mainField.BlackFiguresList.Count(f => f != null);
        else
            alliesCount = _mainField.WhiteFiguresList.Count(f => f != null);

        float modifier = _baseOwnerDamage * (MainField.figureCount / 2 - alliesCount) * _multiplyPerOneDeadAlly;
        _owner.Damage = (int)(_baseOwnerDamage + modifier);
    }
}