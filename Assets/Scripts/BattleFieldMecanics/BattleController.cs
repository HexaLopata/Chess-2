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

    #endregion

    #region private Field
    
    private BattleInfo _battleInfo;
    private BattleFieldFigure _currentFigure;
    
    [SerializeField] private BattleField _field;
    [SerializeField] private SceneTransition _sceneTransition;

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
        
        var turns = _currentFigure.GetRelevantMoves(_field.BattleFieldCells);
        
        if(turns.Length == 0)
            SwitchTurn();
        else
            _field.ActivateAllCells(turns);
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
        StartCoroutine(SetBattleResultWithAnimation(team));
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
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(3));
    }
    
    #endregion
}