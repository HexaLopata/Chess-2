using UnityEngine;

public class ShadowCloning : Skill
{
    [SerializeField] private ShadowClone _shadowClone;

    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if(GetMirrorCell(figure).BattleFieldFigure == null)
        {
            var shadowClone = Instantiate(_shadowClone, figure.BattleField.transform);
            shadowClone.Team = figure.Data.Team;
            shadowClone.Owner = figure;
            _controller.SwitchTurn();
        }
        else
            _delay = 0;
    }

    private BattleFieldCell GetMirrorCell(BattleFieldFigure owner)
    {
        Vector2Int currentOwnerPosition = owner.OnBoardPosition;
        var mirrorCellVector = new Vector2Int(owner.BattleField.Width - 1 - currentOwnerPosition.x,
                                              owner.BattleField.Height - 1 - currentOwnerPosition.y);
        var mirrorCell = owner.BattleField.BattleFieldCells[mirrorCellVector.x, mirrorCellVector.y];
        return mirrorCell;
    }

    public override void Activate(BattleFieldFigure figure)
    {
        Execute(figure, null);
    }
}
