using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Базовый класс для умений фигур
/// </summary>
public abstract class Skill : MonoBehaviour
{
    public UnityEvent activateOrDisactivate;
    public UnityEvent onChangeDelay;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            activateOrDisactivate.Invoke();
        }
    }
    public float Delay
    {
        get => _delay;
        set
        {
            _delay = value;
            onChangeDelay.Invoke();
        }
    }
    public string Name => _name;

    protected BattleController _controller;
    protected float _delay = 0;

    [SerializeField] protected float _maxDelay = 5;
    [SerializeField] protected string _name;

    private bool _isActive;

    /// <summary>
    /// Выполняет действие умения, если умение уже откатилось
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="cell"></param>
    public void Execute(BattleFieldFigure figure, BattleFieldCell cell)
    {
        if (_controller == null)
            SubscribeOnSwitchTurn(figure.BattleField.BattleController);
        if (_delay <= 0)
        {
            _delay = _maxDelay;
            SkillAction(figure, cell);
        }
    }

    /// <summary>
    /// Действие умения
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="cell"></param>
    protected abstract void SkillAction(BattleFieldFigure figure, BattleFieldCell cell);

    /// <summary>
    /// Этот метод выполняется при активации умения
    /// </summary>
    /// <param name="figure"></param>
    public abstract void Activate(BattleFieldFigure figure);

    /// <summary>
    /// Этот метод уменьшает откат каждый ход
    /// </summary>
    protected virtual void UpdateDelay()
    {
        Delay -= 0.5f;
    }

    protected void SubscribeOnSwitchTurn(BattleController controller)
    {
        _controller = controller;
        _controller.onSwitchTurn.AddListener(UpdateDelay);
    }
}