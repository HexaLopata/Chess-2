using UnityEngine;

public class PlaceInAllDirectionsSkill : Skill
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
            var turns = figure.GetRelevantMoves(figure.BattleField.BattleFieldCells);
            foreach (var turn in turns)
            {
                var obj = Instantiate(battleFieldObject, figure.BattleField.transform);
                obj.MoveToAnotherCell(turn);
                obj.Team = figure.Data.Team;
            }

            figure.BattleField.BattleController.SwitchTurn();
            _delay = _maxDelay;
        }
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}