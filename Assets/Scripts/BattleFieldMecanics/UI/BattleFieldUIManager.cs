using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleFieldUIManager : MonoBehaviour
{
    [SerializeField] private Image _firstHealthBar;
    [SerializeField] private Image _secondHealthBar;
    [SerializeField] private Image _yourTurnImage1;
    [SerializeField] private Image _yourTurnImage2;
    [SerializeField] private BattleController _battleController;
    private BattleFieldFigure _firstFigure;
    private BattleFieldFigure _secondFigure;
    
    private void Start()
    {
        _firstFigure = _battleController.BattleInfo.FirstFigure.BattleFieldFigureInstance;
        _secondFigure = _battleController.BattleInfo.SecondFigure.BattleFieldFigureInstance;
        _firstFigure.onTakeDamage.AddListener(UpdateHealthBars);
        _secondFigure.onTakeDamage.AddListener(UpdateHealthBars);
        _battleController.onSwitchTurn.AddListener(ShowCurrentTurn);
    }

    private void UpdateHealthBars()
    {
        StartCoroutine(HPDecreaseAnimation(_firstHealthBar, (float)_firstFigure.Health / 100));
        StartCoroutine(HPDecreaseAnimation(_secondHealthBar, (float)_secondFigure.Health / 100));
    }
    
    private void ShowCurrentTurn()
    {
        if (_battleController.CurrentTurn == Team.Black)
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

    private IEnumerator HPDecreaseAnimation(Image healthBar, float targetHP)
    {
        while (healthBar.fillAmount > targetHP)
        {
            healthBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
