using UnityEngine;

public class TurnManager
{
    public MainFieldFigure SelectedFigure { get; set; }
    public Team CurrentTurn { get; private set; } = Team.White;

    private Field _field;

    public TurnManager(Field field)
    {
        _field = field;
    }

    public void SwitchTurn()
    {
        if (CurrentTurn == Team.Black)
            CurrentTurn = Team.White;
        else
            CurrentTurn = Team.Black;
        SelectedFigure = null;
        _field.DeactivateAllCells();
    }

    public void OnCellClick(Cell cell)
    {
        // Если на клетке есть фигура, то нет смысла взаимодействовать именно с ней
        if (cell.Figure != null)
        {
            OnFigureClick(cell.Figure);
        }
        else
        {
            // Если клекта считается активной для хода, то произвести ход
            if (cell.State == CellState.Active)
            {
                SelectedFigure.MoveToAnotherCell(cell);
                SwitchTurn();
            }
        }
    }

    public void OnFigureClick(MainFieldFigure figure)
    {
        // Если эта фигура не выбрана, то либо:
        if (SelectedFigure != figure)
        {
            // 1. Сделать выбранной и активировать возможные для хода клетки, если сейчас ход команды этой фигуры
            if (CurrentTurn == figure.Data.Team)
            {
                SelectedFigure = figure;
                var turns = figure.GetRelevantTurn(_field.Cells);
                _field.ActivateCells(turns);
                return;
            }

            // 2. Начать битву, если выбранная фигура уже есть и клетка, на которой стоит нажатая фигура активна
            if (SelectedFigure != null && figure.Cell.State == CellState.Active)
            {
                // ToDo Реализовать переход в режим битвы
                Debug.Log("Battle");
                SwitchTurn();
            }
        }
    }
}