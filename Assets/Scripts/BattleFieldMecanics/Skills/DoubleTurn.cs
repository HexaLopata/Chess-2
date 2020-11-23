using System.Collections;

public class DoubleTurn : Skill
{
    public override string Name => "Jerk";
    public override void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
        
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
