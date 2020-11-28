using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainFieldTurnManager : MonoBehaviour
{
    public UnityEvent onSwitchTurn;
    public UnityEvent onSelectFigure;

    public MainFieldFigure SelectedFigure
    {
        get => _selectedFigure;
        set
        {
            _selectedFigure = value;
            onSelectFigure.Invoke();
        }
    }
    public Team CurrentTurn { get; private set; } = Team.White;

    [SerializeField] private MainField _mainField;

    private MainFieldFigure _selectedFigure;

    /// <summary>
    /// Деактивирует все клетки и передает ход другой команде
    /// </summary>
    public void SwitchTurn()
    {
        if (CurrentTurn == Team.Black)
        {
            CurrentTurn = Team.White;
        }
        else
        {
            CurrentTurn = Team.Black;
        }

        SelectedFigure = null;
        _mainField.DeactivateAllCells();
        onSwitchTurn?.Invoke();
    }

    /// <summary>
    /// Этот метод нужен, чтобы менеджер ходов определял нажатия на клетки и принимал соответствующие действия
    /// </summary>
    /// <param name="cellBase"></param>
    public void OnCellClick(CellBase cellBase)
    {
        // Если на клетке есть фигура, то нет смысла взаимодействовать именно с ней
        if (cellBase.Figure != null)
        {
            OnFigureClick(cellBase.Figure.MainFieldFigureInstance);
        }
        else
        {
            // Если клетка считается активной для хода, то произвести ход
            if (cellBase.State == CellState.Active)
            {
                StartCoroutine(SelectedFigure.MoveToAnotherCellWithAnimation(cellBase));
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
            if (SelectedFigure != null && figure.CellBase.State == CellState.Active)
            {
                if (Core.GameMode == GameMode.Normal)
                {
                    var newCell = figure.CellBase;
                    figure.DestroyThisFigure();
                    StartCoroutine(SelectedFigure.MoveToAnotherCellWithAnimation(newCell));
                }
                else
                {
                    // Инициализируем и начинаем бой между фигурами
                    Core.BattleInfo.SetAllInitialInfo(SelectedFigure.Data, figure.Data, figure.CellBase);
                    _mainField.SceneTransition.PlayClose(StartBattle);

                }

                SwitchTurn();
            }
        }
        else
        {
            SelectedFigure = null;
            _mainField.DeactivateAllCells();
        }
    }

    private void StartBattle()
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        _mainField.CurrentCamera.enabled = false;
        Core.BattleInfo.BattleEnd += EndBattle;
    }

    /// <summary>
    /// Применяет все изменения полученные в результате битвы фигур
    /// </summary>
    private void EndBattle()
    {
        if (_mainField.WhiteKings.Count(k => k != null) == 0 &&
            _mainField.BlackKings.Count(k => k != null) == 0)
        {
            SetGameResultAndFinishGame(GameResult.Draw);
        }
        else if (_mainField.WhiteKings.Count(k => k != null) == 0)
        {
            SetGameResultAndFinishGame(GameResult.BlackWon);
        }
        else if (_mainField.BlackKings.Count(k => k != null) == 0)
        {
            SetGameResultAndFinishGame(GameResult.WhiteWon);
        }
        else
        {
            _mainField.CurrentCamera.enabled = true;
            _mainField.SceneTransition.PlayOpen();
            var cell = Core.BattleInfo.CellBaseFightingFor;
            if(Core.BattleInfo.Winner.MainFieldFigureInstance != null)
                StartCoroutine(Core.BattleInfo.Winner.MainFieldFigureInstance.MoveToAnotherCellWithAnimation(cell));
        }
        Core.BattleInfo.BattleEnd -= EndBattle;
    }

    private void SetGameResultAndFinishGame(GameResult result)
    {
        Core.GameResult = result;
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(2));
    }
}