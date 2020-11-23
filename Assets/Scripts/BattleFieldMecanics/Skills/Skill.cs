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
    public abstract string Name { get; }
    
    protected BattleController _controller;
    [SerializeField] protected float _maxDelay = 5;
    protected float _delay = 0;
    
    private bool _isActive;

    public abstract void Execute(BattleFieldFigure figure, BattleFieldCell cell);

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