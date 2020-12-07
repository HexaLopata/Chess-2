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
    public Team CurrentTurn { get; private set; } = Team.No;
    public BattleFieldFigure CurrentFigure => _currentFigure;
    public BattleInfo BattleInfo => _battleInfo;
    public BattleField BattleField => _field;
    public int TurnCount
    {
        get => _turnCount;
        set
        {
            if (_turnCount >= _maxTurnCount)
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

    [SerializeField] private int _suddenDeathDamage = 30;
    [SerializeField] private int _maxTurnCount = 20; 
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
    /// Передает ход переданной команде
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

        _field.DeactivateAllCells();
        if (_currentFigure.Data.Health > 0)
        {
            var turns = _currentFigure.GetRelevantMoves();

            if (turns.Length == 0)
                SwitchTurn();
            else
            {
                _field.ActivateAllCells(turns);
                TurnCount++;
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
    /// Выставляет результаты битвы и переключает сцену на главное поле
    /// </summary>
    /// <param name="team">Победитель</param>
    public void SetBattleResult(Team team)
    {
        if(_settingBattleResult == null)
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
        _field.DeactivateAllCells();
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
        
        var turns = _currentFigure.GetRelevantMoves();
        _field.ActivateAllCells(turns);
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