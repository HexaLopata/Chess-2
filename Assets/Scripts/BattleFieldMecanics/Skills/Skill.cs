using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������� ����� ��� ������ �����
/// </summary>
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
    public float Delay => _delay;
    public string Name => _name;

    protected BattleController _controller;
    protected float _delay = 0;

    [SerializeField] protected float _maxDelay = 5;
    [SerializeField] protected string _name;

    private bool _isActive;

    /// <summary>
    /// ��������� �������� ������, ���� ������ ��� ����������
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
    /// �������� ������
    /// </summary>
    /// <param name="figure"></param>
    /// <param name="cell"></param>
    protected abstract void SkillAction(BattleFieldFigure figure, BattleFieldCell cell);

    /// <summary>
    /// ���� ����� ����������� ��� ��������� ������
    /// </summary>
    /// <param name="figure"></param>
    public abstract void Activate(BattleFieldFigure figure);

    /// <summary>
    /// ���� ����� ��������� ����� ������ ���
    /// </summary>
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