using UnityEngine;

public class PlaceBattleFieldObject : Skill
{
    [SerializeField] private BattleFieldObject battleFieldObject;
    private BattleController _controller;
    private float _maxDelay = 5;
    private float _delay = 0;
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
            var obj = Instantiate(battleFieldObject, figure.BattleField.transform);
            obj.MoveToAnotherCell(cell);
            obj.Team = figure.Data.Team;
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

    private void UpdateDelay()
    {
        _delay -= 0.5f;
    }

    private void SubscribeOnSwitchTurn(BattleController controller)
    {
        _controller = controller;
        _controller.onSwitchTurn.AddListener(UpdateDelay);
    }
}