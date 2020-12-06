using UnityEngine;

public class PlaceInAllDirectionsSkill : Skill
{
    [SerializeField] private BattleFieldObject battleFieldObject;
    [SerializeField] private bool _continueTurn;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var turns = figure.GetRelevantMoves();
        foreach (var turn in turns)
        {
            var obj = Instantiate(battleFieldObject, figure.BattleField.transform);
            obj.MoveToAnotherCell(turn);
            obj.Team = figure.Data.Team;
        }

        if (!_continueTurn)
            figure.BattleField.BattleController.SwitchTurn();
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}