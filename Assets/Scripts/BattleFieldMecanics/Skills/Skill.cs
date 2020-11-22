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

    private bool _isActive = false;

    public abstract void Execute(BattleFieldFigure figure, BattleFieldCell cell);

    public abstract void Activate(BattleFieldFigure figure);
}
