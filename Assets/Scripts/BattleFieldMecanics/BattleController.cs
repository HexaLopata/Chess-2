using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    #region Events

    public UnityEvent onSwitchTurn;

    #endregion

    #region public Properties

    public Team CurrentTurn { get; private set; } = Team.White;
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
                StartCoroutine(SuddenDeathAnimation());
            }

            _turnCount = value;
        }
    }

    #endregion

    #region private Field
    
    private BattleInfo _battleInfo;
    private BattleFieldFigure _currentFigure;
    private Coroutine _settingBattleResult;
    private int _turnCount;
    private const int _maxTurnCount = 30; 
    
    [SerializeField] private BattleField _field;
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private Chess2Text _suddenDeathMessage1;
    [SerializeField] private Chess2Text _suddenDeathMessage2;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _battleInfo = Core.BattleInfo;
        _sceneTransition.PlayOpen(StartBattle);
    }

    #endregion

    #region public Methods

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
            var turns = _currentFigure.GetRelevantMoves(_field.BattleFieldCells);

            if (turns.Length == 0)
                SwitchTurn();
            else
            {
                _field.ActivateAllCells(turns);
                TurnCount++;
            }
        }
    }
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
    
    public void SetBattleResult(Team team)
    {
        if(_settingBattleResult == null)
            _settingBattleResult = StartCoroutine(SetBattleResultWithAnimation(team));
    }

  
    #endregion

    #region private Methods

    private void StartBattle()
    {
        var figureFirst = Core.BattleInfo.FirstFigure.BattleFieldFigureInstance;
        foreach (var cell in BattleField.BattleFieldCells)
        {
            cell.TakeDamage(figureFirst);
        }
        
        SwitchTurn(figureFirst.Data.Team);
    }

    private IEnumerator SuddenDeathAnimation()
    {
        _field.DeactivateAllCells();
        _suddenDeathMessage1.gameObject.SetActive(true);
        _suddenDeathMessage2.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2);
        
        _suddenDeathMessage1.gameObject.SetActive(false);
        _suddenDeathMessage2.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);
        
        if (BattleField.FirstFigure.Health > BattleField.SecondFigure.Health)
        {
            BattleField.SecondFigure.TakeDamage(1000);
        }
        else if (BattleField.FirstFigure.Health < BattleField.SecondFigure.Health)
        {
            BattleField.FirstFigure.TakeDamage(1000);
        }
        
        var turns = _currentFigure.GetRelevantMoves(_field.BattleFieldCells);
        _field.ActivateAllCells(turns);
    }
    
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
    
    #endregion
}