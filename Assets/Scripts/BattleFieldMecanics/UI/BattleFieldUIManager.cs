using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleFieldUIManager : MonoBehaviour
{
    [SerializeField] private Image _firstHealthBar;
    [SerializeField] private Image _secondHealthBar;
    [SerializeField] private Chess2Text _yourTurnText1;
    [SerializeField] private Chess2Text _yourTurnText2;
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

    private void UpdateHealthBars()
    {
        StartCoroutine(HPDecreaseAnimation(_firstHealthBar, (float)_firstFigure.Health / 100));
        StartCoroutine(HPDecreaseAnimation(_secondHealthBar, (float)_secondFigure.Health / 100));
    }
    
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
