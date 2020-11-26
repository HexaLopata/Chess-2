using System.Collections;

public class DoubleTurn : Skill
{
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        base.Execute(figure, cell);
        
        if (_delay <= 0)
        {
            StartCoroutine(figure.MoveToAnotherCellWithAnimation(cell));
            _delay = _maxDelay;
            _controller.BattleField.ActivateAllCells(figure.GetRelevantMoves(_controller.BattleField.BattleFieldCells));
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
