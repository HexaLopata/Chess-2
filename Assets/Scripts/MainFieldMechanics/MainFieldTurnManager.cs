using UnityEngine;
using UnityEngine.SceneManagement;

public class MainFieldTurnManager
{
    public MainFieldFigure SelectedFigure { get; set; }
    public Team CurrentTurn { get; private set; } = Team.White;

    private MainField _mainField;

    public MainFieldTurnManager(MainField mainField)
    {
        _mainField = mainField;
    }

    /// <summary>
    /// Деактивирует все клетки и передает ход другой команде
    /// </summary>
    public void SwitchTurn()
    {
        if (CurrentTurn == Team.Black)
            CurrentTurn = Team.White;
        else
            CurrentTurn = Team.Black;
        SelectedFigure = null;
        _mainField.DeactivateAllCells();
    }

    /// <summary>
    /// Этот метод нужен, чтобы менеджер ходов определял нажатия на клетки и принимал соответствующие действия
    /// </summary>
    /// <param name="cell"></param>
    public void OnCellClick(Cell cell)
    {
        // Если на клетке есть фигура, то нет смысла взаимодействовать именно с ней
        if (cell.Figure != null)
        {
            OnFigureClick(cell.Figure.MainFieldFigureInstance);
        }
        else
        {
            // Если клетка считается активной для хода, то произвести ход
            if (cell.State == CellState.Active)
            {
                SelectedFigure.MoveToAnotherCell(cell);
                SwitchTurn();
            }
        }
    }

    /// <summary>
    /// Этот метод нужен, чтобы менеджер ходов определял нажатия на фигуры и принимал соответствующие действия
    /// </summary>
    /// <param name="figure"></param>
    public void OnFigureClick(MainFieldFigure figure)
    {
        // Если эта фигура не выбрана, то либо:
        if (SelectedFigure != figure)
        {
            // 1. Сделать выбранной и активировать возможные для хода клетки, если сейчас ход команды этой фигуры
            if (CurrentTurn == figure.Data.Team)
            {
                SelectedFigure = figure;
                var turns = figure.GetRelevantTurn(_mainField.Cells);
                _mainField.ActivateCells(turns);
                return;
            }

            // 2. Начать битву, если выбранная фигура уже есть и клетка, на которой стоит нажатая фигура активна
            if (SelectedFigure != null && figure.Cell.State == CellState.Active)
            {
                if (Core.GameMode == GameMode.Normal)
                {
                    var newCell = figure.Cell;
                    figure.DestroyThisFigure();
                    SelectedFigure.MoveToAnotherCell(newCell);
                }
                else
                {
                    // Инициализируем и начинаем бой между фигурами
                    Core.BattleInfo.SetAllInitialInfo(SelectedFigure.Data, figure.Data, figure.Cell);
                    StartBattle();
                }
                SwitchTurn();
            }
        }
    }

    private void StartBattle()
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        Core.BattleInfo.BattleEnd += EndBattle;
    }

    /// <summary>
    /// Применяет все изменения полученные в результате битвы фигур
    /// </summary>
    public void EndBattle()
    {
        Core.BattleInfo.BattleEnd -= EndBattle;
        Core.BattleInfo.Loser.MainFieldFigureInstance.DestroyThisFigure();
        var cell = Core.BattleInfo.CellFightingFor;
        Core.BattleInfo.Winner.MainFieldFigureInstance.MoveToAnotherCell(cell);
    }
}