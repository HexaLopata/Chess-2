using UnityEngine;

public class PlaceBattleFieldObject : Skill
{
    [SerializeField] private BattleFieldObject _battleFieldObject;
    [SerializeField] private bool _continueTurn;
    
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        base.Execute(figure, cell);
        if (_delay <= 0)
        {
            var obj = Instantiate(_battleFieldObject, figure.BattleField.transform);
            obj.MoveToAnotherCell(cell);
            obj.Team = figure.Data.Team;
            if (!_continueTurn)
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