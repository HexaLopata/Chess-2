using UnityEngine;

public class PlaceBattleFieldObjectUnderFigure : Skill
{
    [SerializeField] private BattleFieldObject _battleFieldObject;
    [SerializeField] private bool _continueTurn;

    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        base.Execute(figure, cell);
        
        if (_delay <= 0)
        {
            var obj = Instantiate(_battleFieldObject, figure.BattleField.transform);
            obj.MoveToAnotherCell(figure.BattleField.BattleFieldCells[figure.OnBoardPosition.x, figure.OnBoardPosition.y]);
            obj.Team = figure.Data.Team;
            if(!_continueTurn)
                figure.BattleField.BattleController.SwitchTurn();
            _delay = _maxDelay;
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
