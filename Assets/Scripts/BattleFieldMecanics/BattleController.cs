using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public UnityEvent onSwitchTurn;
    public Team CurrentTurn { get; private set; } = Team.White;
    public BattleFieldFigure CurrentFigure => _currentFigure;
    public BattleInfo BattleInfo => _battleInfo;
    
    private BattleInfo _battleInfo;
    [SerializeField] private BattleField _field;
    private BattleFieldFigure _currentFigure;

    private void Start()
    {
        _battleInfo = Core.BattleInfo;
    }

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
        var turns = _currentFigure.GetRelevantMoves(_field.BattleFieldCells);
        
        if(turns.Length == 0)
            SwitchTurn();
        else
            _field.ActivateAllCells(turns);
        
        onSwitchTurn.Invoke();
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
        if (_field.FirstFigure.Data.Team == team)
        {
            _field.SecondFigure.Data.Health = _field.SecondFigure.Health;
            _battleInfo.Loser = _field.SecondFigure.Data;
            Destroy(_field.SecondFigure);
            _battleInfo.Winner = _field.FirstFigure.Data;
        }
        else
        {
            _battleInfo.Loser = _field.FirstFigure.Data;
            Destroy(_field.FirstFigure);
            _battleInfo.Winner = _field.SecondFigure.Data;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(3));
    }
}