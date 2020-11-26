using UnityEngine;

public class PlaceInAllDirectionsSkill : Skill
{
    [SerializeField] private BattleFieldObject battleFieldObject;
    [SerializeField] private bool _continueTurn;

    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        base.Execute(figure, cell);
        if (_delay <= 0)
        {
            var turns = figure.GetRelevantMoves(figure.BattleField.BattleFieldCells);
            foreach (var turn in turns)
            {
                var obj = Instantiate(battleFieldObject, figure.BattleField.transform);
                obj.MoveToAnotherCell(turn);
                obj.Team = figure.Data.Team;
            }

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