using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Главный класс для управления интерфейсом поля битвы
/// </summary>
public class BattleFieldUIManager : MonoBehaviour
{
    [SerializeField] private Image _firstHealthBar;
    [SerializeField] private Image _secondHealthBar;
    [SerializeField] private Chess2Text _yourTurnText1;
    [SerializeField] private Chess2Text _yourTurnText2;
    [SerializeField] private Chess2Text _skill1Recovery;
    [SerializeField] private Chess2Text _skill2Recovery;
    [SerializeField] private BattleController _battleController;
    private BattleFieldFigure _firstFigure;
    private BattleFieldFigure _secondFigure;
    
    private void Start()
    {
        _firstFigure = _battleController.BattleField.FirstFigure;
        _secondFigure = _battleController.BattleField.SecondFigure;
        _firstFigure.onTakeDamage.AddListener(UpdateHealthBars);
        _secondFigure.onTakeDamage.AddListener(UpdateHealthBars);
        _battleController.onSwitchTurn.AddListener(ShowCurrentTurn);
        UpdateHealthBars();
        ShowCurrentTurn();
    }

    /// <summary>
    /// Обновляет шкалы здоровья
    /// </summary>
    private void UpdateHealthBars()
    {
        StartCoroutine(HPDecreaseAnimation(_firstHealthBar, (float)_firstFigure.Health / 100));
        StartCoroutine(HPDecreaseAnimation(_secondHealthBar, (float)_secondFigure.Health / 100));
    }
    
    /// <summary>
    /// Показывает, чей сейчас ход
    /// </summary>
    private void ShowCurrentTurn()
    {
        if (_battleController.CurrentTurn == Team.Black)
        {
            _yourTurnText1.gameObject.SetActive(false);
            _yourTurnText2.gameObject.SetActive(true);
        }
        else
        {
            _yourTurnText1.gameObject.SetActive(true);
            _yourTurnText2.gameObject.SetActive(false);
        }

        ShowSkillDelay();
    }

    /// <summary>
    /// Показывает, сколько осталось до отката умения
    /// </summary>
    private void ShowSkillDelay()
    {
        if (_battleController.BattleField.FirstFigure != null)
        {
            if (_battleController.BattleField.FirstFigure.Skill.Delay > 0.5f)
                _skill1Recovery.Text = Math.Floor(_battleController.BattleField.FirstFigure.Skill.Delay).ToString();
            else
                _skill1Recovery.Text = "";
        }

        if (_battleController.BattleField.FirstFigure != null)
        {
            if (_battleController.BattleField.SecondFigure.Skill.Delay > 0.5f)
                _skill2Recovery.Text = Math.Floor(_battleController.BattleField.SecondFigure.Skill.Delay).ToString();
            else
                _skill2Recovery.Text = "";
        }
    }

    /// <summary>
    /// Анимирует уменьшение жизней в шкале здоровья
    /// </summary>
    /// <param name="healthBar"></param>
    /// <param name="targetHP"></param>
    /// <returns></returns>
    private IEnumerator HPDecreaseAnimation(Image healthBar, float targetHP)
    {
        while (healthBar.fillAmount > targetHP)
        {
            healthBar.fillAmount -= 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
