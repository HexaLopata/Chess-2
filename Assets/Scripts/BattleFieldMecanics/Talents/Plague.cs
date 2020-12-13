using UnityEngine;

public class Plague : Talent
{
    [SerializeField] private int _plagueDamage = 10;
    protected override void TalentAction()
    {
        var position = _owner.OnBoardPosition;
        AttackCellIfItsExists(position.x + 1, position.y + 1);
        AttackCellIfItsExists(position.x + 1, position.y);
        AttackCellIfItsExists(position.x + 1, position.y - 1);
        AttackCellIfItsExists(position.x - 1, position.y + 1);
        AttackCellIfItsExists(position.x - 1, position.y);
        AttackCellIfItsExists(position.x - 1, position.y - 1);
        AttackCellIfItsExists(position.x, position.y + 1);
        AttackCellIfItsExists(position.x, position.y - 1);
    }

    private void AttackCellIfItsExists(int x, int y)
    {
        if (_owner.BattleField.IsCellExists(x, y))
            _owner.BattleField.BattleFieldCells[x, y].TakeDamage(_plagueDamage);
    }
}

