using System;
using UnityEngine.EventSystems;

/// <summary>
/// Эта клетка используется исключительно на главном поле
/// </summary>
public class MainFieldCell : CellBase
{
    private MainField _mainField;

    private void Start()
    {
        if(_field is MainField)
            _mainField = (MainField)_field;
        else
        {
            throw new Exception("Клетка типа MainFieldCell должна быть дочереней от поля класса MainField или его подклассов");
        }
    }

    /// <summary>
    /// Передает менеджеру ходов информацию о нажатии на текущую клетку
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerClick(PointerEventData eventData)
    {
        var field = GetComponentInParent<MainField>();
        field.MainFieldTurnManager.OnCellClick(this);
    }
    
    /// <summary>
    /// Делает клетку активной для совершения хода на нее текущей фигурой
    /// </summary>
    public override void Activate()
    {
        State = CellState.Active;
        if (_mainField.MainFieldTurnManager.SelectedFigure.Data != Figure)
        {
            if (Figure != null)
            {
                if (_mainField.MainFieldTurnManager.CurrentTurn != Figure.Team)
                    _image.sprite = _enemy;
            }
            else
                _image.sprite = _active;
        }
    }

    /// <summary>
    /// Делает клетку не активной для совершения хода на нее текущей фигурой
    /// </summary>
    public override void Deactivate()
    {
        State = CellState.NotActive;
        _image.sprite = _normal;
    }
}
