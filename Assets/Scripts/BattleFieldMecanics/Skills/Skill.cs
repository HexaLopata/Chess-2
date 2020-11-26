using UnityEngine;
using UnityEngine.Events;

public abstract class Skill : MonoBehaviour
{
    public UnityEvent activateOrDisactivate;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            activateOrDisactivate.Invoke();
        }
    }
    public string Name => _name;
    
    protected BattleController _controller;
    protected float _delay = 0;
    
    [SerializeField] protected float _maxDelay = 5;
    [SerializeField] protected string _name;
    
    private bool _isActive;

    public virtual void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
    }

    public abstract void Activate(BattleFieldFigure figure);
    
    protected virtual void UpdateDelay()
    {
        _delay -= 0.5f;
    }

    protected void SubscribeOnSwitchTurn(BattleController controller)
    {
        _controller = controller;
        _controller.onSwitchTurn.AddListener(UpdateDelay);
    }
}