using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Основной класс для работы с интерфейсом на главной сцене
/// </summary>
public class MainFieldUIManager : MonoBehaviour
{
    [SerializeField] private Chess2Text _yourTurnText1;
    [SerializeField] private Chess2Text _yourTurnText2;
    [SerializeField] private MainFieldTurnManager _turnManager;

    [Header("Info Borders")]
    [SerializeField] private Image _whiteIcon;
    [SerializeField] private Image _blackIcon;
    [SerializeField] private Chess2Text _whiteName;
    [SerializeField] private Chess2Text _blackName;
    [SerializeField] private Chess2Text _whiteHP;
    [SerializeField] private Chess2Text _blackHP;
    [SerializeField] private Chess2Text _whiteSkillName;
    [SerializeField] private Chess2Text _blackSkillName;
    [SerializeField] private Image _whiteInfoBorder;
    [SerializeField] private Image _blackInfoBorder;

    private void Start()
    {
        // Убирает информационные таблички, если игровой режим - классический
        if (Core.GameMode == GameMode.Normal)
        {
            _whiteInfoBorder.gameObject.SetActive(false);
            _blackInfoBorder.gameObject.SetActive(false);
        }
        DisableAllBorderInfo();
    }

    /// <summary>
    /// Включает и выключает таблички с надписями о текущем ходе
    /// </summary>
    public void ShowCurrentTurn()
    {
        if (_turnManager.CurrentTurn == Team.Black)
        {
            _yourTurnText1.gameObject.SetActive(false);
            _yourTurnText2.gameObject.SetActive(true);
        }
        else
        {
            _yourTurnText1.gameObject.SetActive(true);
            _yourTurnText2.gameObject.SetActive(false);
        }

        DisableAllBorderInfo();
    }

    /// <summary>
    /// Обновляет всю информацию на табличках
    /// </summary>
    public void UpdateBorderInfo()
    {
        if (_turnManager.SelectedFigure != null)
        {
            if (_turnManager.CurrentTurn == Team.White)
            {
                EnableAllWhiteBorderInfo();
                _whiteIcon.sprite = _turnManager.SelectedFigure.GetComponent<Image>().sprite;
                _whiteName.Text = _turnManager.SelectedFigure.FigureName;
                _whiteHP.Text = $"HP {_turnManager.SelectedFigure.Data.Health}I100";
                _whiteSkillName.Text = _turnManager.SelectedFigure.Data.BattleFieldFigurePrefub.Skill.Name;
            }
            else
            {
                EnableAllBlackBorderInfo();
                _blackIcon.sprite = _turnManager.SelectedFigure.GetComponent<Image>().sprite;
                _blackName.Text = _turnManager.SelectedFigure.FigureName;
                _blackHP.Text = $"HP {_turnManager.SelectedFigure.Data.Health}I100";
                _blackSkillName.Text = _turnManager.SelectedFigure.Data.BattleFieldFigurePrefub.Skill.Name;
            }
        }
        else
            DisableAllBorderInfo();

    }

    /// <summary>
    /// Убирает всю информацию с табличек
    /// </summary>
    private void DisableAllBorderInfo()
    {
        _whiteIcon.enabled = false;
        _blackIcon.enabled = false;
        _whiteName.gameObject.SetActive(false);
        _blackName.gameObject.SetActive(false);
        _whiteHP.gameObject.SetActive(false);
        _blackHP.gameObject.SetActive(false);
        _whiteSkillName.gameObject.SetActive(false);
        _blackSkillName.gameObject.SetActive(false);
    }

    private void EnableAllWhiteBorderInfo()
    {
        _whiteIcon.enabled = true;
        _whiteName.gameObject.SetActive(true);
        _whiteHP.gameObject.SetActive(true);
        _whiteSkillName.gameObject.SetActive(true);
    }

    private void EnableAllBlackBorderInfo()
    {
        _blackIcon.enabled = true;
        _blackName.gameObject.SetActive(true);
        _blackHP.gameObject.SetActive(true);
        _blackSkillName.gameObject.SetActive(true);
    }
}