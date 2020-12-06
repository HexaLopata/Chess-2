using UnityEngine;

public class PlaceBattleFieldObject : Skill
{
    [SerializeField] private BattleFieldObject _battleFieldObject;
    [SerializeField] private bool _continueTurn;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var obj = Instantiate(_battleFieldObject, figure.BattleField.transform);
        obj.MoveToAnotherCell(cell);
        obj.Team = figure.Data.Team;
        if (!_continueTurn)
            figure.BattleField.BattleController.SwitchTurn();

    }

    public override void Activate(BattleFieldFigure figure)
    {
        if (_delay <= 0 || IsActive == true)
        {
            IsActive = !IsActive;
        }
    }
}