using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public event Action BattleEnd;
    private BattleInfo _battleInfo;
    [SerializeField] private Image _firstHealthBar;
    [SerializeField] private Image _secondHealthBar;
    [SerializeField] private BattleField _field;
    public Team CurrentTurn { get; private set; } = Team.White;
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
        
        _firstHealthBar.fillAmount = (float)_field.FirstFigure.Health / 100;
        _secondHealthBar.fillAmount = (float)_field.SecondFigure.Health / 100;
    }
    
    public void SwitchTurn()
    {
        if (CurrentTurn == Team.Black)
        {
            CurrentTurn = Team.White;
            _currentFigure = _field.FirstFigure;
        }
        else
        {
            CurrentTurn = Team.Black;
            _currentFigure = _field.SecondFigure;
        }
        var turns = _currentFigure.GetRelevantMoves(_field.BattleFieldCells);
        if(turns.Length == 0)
            SwitchTurn();
        else
            _field.ActivateAllCells(turns);
        
        _firstHealthBar.fillAmount = (float)_field.FirstFigure.Health / 100;
        _secondHealthBar.fillAmount = (float)_field.SecondFigure.Health / 100;
    }

    public void OnCellClick(BattleFieldCell battleFieldCell)
    {
        if (battleFieldCell.State == CellState.Active)
        {
            _currentFigure.MoveToAnotherCell(battleFieldCell);
            LaunchAnAttack(_currentFigure.GetRelevantAttackMoves(_field.BattleFieldCells), _currentFigure);
            SwitchTurn();
        }
    }

    public void LaunchAnAttack(BattleFieldCell[] battleFieldCells, BattleFieldFigure attacker)
    {
        foreach (var cell in battleFieldCells)
        {
            cell.TakeDamage(attacker);
        }
    }

    public void SetBattleResult(Team team)
    {
        _battleInfo.FirstFigure.Health = _field.FirstFigure.Health;
        _battleInfo.SecondFigure.Health = _field.SecondFigure.Health;
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