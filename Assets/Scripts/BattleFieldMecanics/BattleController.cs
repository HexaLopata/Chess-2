using System;
using System.Collections;
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
    [SerializeField] private Image _yourTurnImage1;
    [SerializeField] private Image _yourTurnImage2;
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
        
        UpdateUI();
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
        UpdateUI();
    }

    private void UpdateUI()
    {
        StartCoroutine(HPDecreaseAnimation(_firstHealthBar, (float)_field.FirstFigure.Health / 100));
        StartCoroutine(HPDecreaseAnimation(_secondHealthBar, (float)_field.SecondFigure.Health / 100));
        if (CurrentTurn == Team.Black)
        {
            _yourTurnImage1.enabled = false;
            _yourTurnImage2.enabled = true;
        }
        else
        {
            _yourTurnImage1.enabled = true;
            _yourTurnImage2.enabled = false;
        }
    }

    public IEnumerator HPDecreaseAnimation(Image healthBar, float targetHP)
    {
        while (healthBar.fillAmount > targetHP)
        {
            healthBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
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