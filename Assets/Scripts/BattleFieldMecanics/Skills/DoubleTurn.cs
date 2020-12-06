public class DoubleTurn : Skill
{
    protected override void SkillAction(BattleFieldFigure figure, BattleFieldCell cell)
    {
        StartCoroutine(figure.MoveToAnotherCellWithAnimation(cell));
        _controller.BattleField.ActivateAllCells(figure.GetRelevantMoves());
    }
    
    public override void Activate(BattleFieldFigure figure)
    {
        if (_delay <= 0 || IsActive)
        {
            IsActive = !IsActive;
        }
    }
}
