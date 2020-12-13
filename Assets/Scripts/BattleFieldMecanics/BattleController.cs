using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Этот класс отвечает за проведения ходов на поле битвы
/// </summary>
public class BattleController : MonoBehaviour
{
    public UnityEvent onSwitchTurn;
    public Team CurrentTurn
    {
        get
        {
            if (_fightingLoopIsActive)
                return _currentTurn;
            return Team.No;
        }
        private set
        {
            _currentTurn = value;
        }
    }

    public BattleFieldFigure CurrentFigure => _currentFigure;
    public BattleInfo BattleInfo => _battleInfo;
    public BattleField BattleField => _field;
    public int TurnCount
    {
        get => _turnCount;
        set
        {
            if (_turnCount == _maxTurnCount)
                _playSuddenDeath = true;

            if (_playSuddenDeath)
            {
                StartCoroutine(SuddenDeathRoutine());
            }

            _turnCount = value;
        }
    }

    private BattleInfo _battleInfo;
    private BattleFieldFigure _currentFigure;
    private Coroutine _settingBattleResult;
    private int _turnCount;
    private Team _currentTurn = Team.No;
    private bool _fightingLoopIsActive = true;

    [SerializeField] private int _suddenDeathDamage = 30;
    [SerializeField] private int _maxTurnCount = 20;
    [SerializeField] private bool _playSuddenDeath = false;
    [SerializeField] private BattleField _field;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private Chess2Text _suddenDeathMessage1;
    [SerializeField] private Chess2Text _suddenDeathMessage2;

    private void Start()
    {
        _battleInfo = Core.BattleInfo;
        _sceneTransition.PlayOpen(OnStartBattle);
    }

    /// <summary>
    /// Дает ход переданной команде
    /// </summary>
    /// <param name="team"></param>
    public void SwitchTurn(Team team)
    {
        if (team == Team.Black)
        {
            CurrentTurn = Team.Black;
            _currentFigure = _field.SecondFigure;
        }
        else
        {
            CurrentTurn = Team.White;
            _currentFigure = _field.FirstFigure;
        }
        onSwitchTurn.Invoke();

        DeactivateAllCells();
        if (_currentFigure.enabled)
        {
            var turns = _currentFigure.GetRelevantMoves();

            if (turns.Length == 0)
                SwitchTurn();
            else
            {
                ActivateAllCells(turns);
                TurnCount++;
            }
        }
    }

    /// <summary>
    /// Активирует все переданные клетки
    /// </summary>
    /// <param name="battleFieldCells"></param>
    public void ActivateAllCells(BattleFieldCell[] battleFieldCells)
    {
        if (_fightingLoopIsActive)
        {
            DeactivateAllCells();
            foreach (var cell in battleFieldCells)
            {
                if (cell != null)
                    cell.Activate();
            }
        }
    }

    /// <summary>
    /// Деактивирует все клетки
    /// </summary>
    public void DeactivateAllCells()
    {
        if (_field.BattleFieldCells != null)
        {
            foreach (var cell in _field.BattleFieldCells)
            {
                if (cell != null)
                    cell.Deactivate();
            }
        }
    }

    /// <summary>
    /// Переключает ход
    /// </summary>
    public void SwitchTurn()
    {
        if (CurrentTurn == Team.Black)
        {
            SwitchTurn(Team.White);
        }
        else
        {
            SwitchTurn(Team.Black);
        }
    }

    /// <summary>
    /// Останавливает цикл ходов
    /// </summary>
    public void StopRequest()
    {
        _fightingLoopIsActive = false;
        DeactivateAllCells();
    }

    /// <summary>
    /// Возобновляет цикл ходов
    /// </summary>
    public void StartRequest()
    {
        _fightingLoopIsActive = true;
        ActivateAllCells(_currentFigure.GetRelevantMoves());
    }

    /// <summary>
    /// Выставляет результаты битвы и переключает сцену на главное поле
    /// </summary>
    /// <param name="team">Победитель</param>
    public void SetBattleResult(Team team)
    {
        if (_settingBattleResult == null)
            _settingBattleResult = StartCoroutine(SetBattleResultWithAnimation(team));
    }

    /// <summary>
    /// Выполняется в начале битвы
    /// </summary>
    private void OnStartBattle()
    {
        var figureFirst = Core.BattleInfo.FirstFigure.BattleFieldFigureInstance;
        foreach (var cell in BattleField.BattleFieldCells)
        {
            if (cell == null)
                Debug.Log("cell is null");
            cell.TakeDamage(figureFirst);
        }

        SwitchTurn(figureFirst.Data.Team);
    }

    /// <summary>
    /// Выполняется во время "Внезапной смерти"
    /// </summary>
    /// <returns></returns>
    private IEnumerator SuddenDeathRoutine()
    {
        StopRequest();
        _suddenDeathMessage1.gameObject.SetActive(true);
        _suddenDeathMessage2.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        _suddenDeathMessage1.gameObject.SetActive(false);
        _suddenDeathMessage2.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        foreach (var cell in BattleField.BattleFieldCells)
        {
            cell.TakeDamage(_suddenDeathDamage);
        }

        StartRequest();
    }

    /// <summary>
    /// Вспомогательный метод для завершения битвы
    /// </summary>
    /// <param name="team"></param>
    /// <returns></returns>
    private IEnumerator SetBattleResultWithAnimation(Team team)
    {
        yield return _sceneTransition.Close();

        if (_field.FirstFigure.Data.Team == team)
        {
            _field.SecondFigure.Data.Health = _field.SecondFigure.Health;
            _battleInfo.Loser = _field.SecondFigure.Data;
            _battleInfo.Winner = _field.FirstFigure.Data;
        }
        else
        {
            _battleInfo.Loser = _field.FirstFigure.Data;
            _battleInfo.Winner = _field.SecondFigure.Data;
        }

        if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        }
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(3));
        _settingBattleResult = null;
    }
}