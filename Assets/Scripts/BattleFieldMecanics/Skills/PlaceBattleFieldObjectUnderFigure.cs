using UnityEngine;

public class PlaceBattleFieldObjectUnderFigure : Skill
{
    [SerializeField] private BattleFieldObject _battleFieldObject;
    [SerializeField] private bool _continueTurn;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        var obj = Instantiate(_battleFieldObject, figure.BattleField.transform);
        obj.MoveToAnotherCell(figure.BattleField.BattleFieldCells[figure.OnBoardPosition.x, figure.OnBoardPosition.y]);
        obj.Team = figure.Data.Team;
        if (!_continueTurn)
            figure.BattleField.BattleController.SwitchTurn();
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
