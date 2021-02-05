using UnityEngine;

public abstract class Talent : MonoBehaviour
{
    public BattleFieldFigure Owner
    {
        get => _owner;
        set
        {
            if (value.Data != null)
            {
                _owner = value;
                _team = _owner.Data.Team;
                _controller = _owner.BattleField.BattleController;
                _controller.onSwitchTurn.AddListener(Execute);
            }
        }
    }
    public string Name => _name;

    [SerializeField] private string _name = "Talent";
    [SerializeField] private bool _inEachTurn = false;
    [SerializeField] private bool _inEnemyTurn = false;

    protected Team _team;
    protected BattleFieldFigure _owner;
    protected BattleController _controller;

    /// <summary>
    /// Вызывает действие таланта, если сейчас подходящий ход
    /// </summary>
    public void Execute()
    {
        if (_owner.enabled)
        {
            if (_inEachTurn)
            {
                TalentAction();
            }
            else
            {
                if (_inEnemyTurn)
                {
                    if (_controller.CurrentTurn == _owner.Data.Team)
                    {
                        TalentAction();
                    }
                }
                else if (_controller.CurrentTurn != _owner.Data.Team && !object.ReferenceEquals(_controller.CurrentFigure.Talent, this))
                {
                    TalentAction();
                }
            }
        }
    }

    /// <summary>
    /// Выполняет действие таланта
    /// </summary>
    protected abstract void TalentAction();
}