using UnityEngine;

public class PlaceBattleFieldObject : Skill
{
    [SerializeField] private BattleFieldObject battleFieldObject;
    public override string Name
    {
        get => battleFieldObject.GetType().Name;
    }

    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
        if (_delay <= 0)
        {
            var obj = Instantiate(battleFieldObject, figure.BattleField.transform);
            obj.MoveToAnotherCell(cell);
            obj.Team = figure.Data.Team;
            figure.BattleField.BattleController.SwitchTurn();
            _delay = _maxDelay;
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        if (_delay <= 0 || IsActive == true)
        {
            IsActive = !IsActive;
        }
    }
}