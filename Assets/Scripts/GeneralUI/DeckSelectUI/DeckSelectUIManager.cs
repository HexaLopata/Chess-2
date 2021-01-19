using UnityEngine;

public class DeckSelectUIManager : MonoBehaviour
{
    public FigureData CurrentFigure { get { return _currentFigure; } }
    private FigureData _currentFigure;

    [SerializeField] private Chess2Text _name;
    [SerializeField] private Chess2Text _damage;
    [SerializeField] private Chess2Text _defence;
    [SerializeField] private Chess2Text _talentName;
    [SerializeField] private Chess2Text _skillName;
    [SerializeField] private GameObject _talentScrollView;
    [SerializeField] private GameObject _skillScrollView;
    [SerializeField] private FigureSelector _figureSelector;


    private void Start()
    {
        UpdateInfo(_figureSelector.Figure);
    }

    public void UpdateInfo(FigureData figure)
    {
        _currentFigure = figure;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        if (_currentFigure != null)
        {
            _name.Text = _currentFigure.MainFieldFigurePrefub.FigureName;
            _damage.Text = _currentFigure.BattleFieldFigurePrefub.Damage.ToString();
            _defence.Text = _currentFigure.BattleFieldFigurePrefub.Defence.ToString();
            _talentName.Text = _currentFigure.BattleFieldFigurePrefub.Talent.Name;
            _skillName.Text = _currentFigure.BattleFieldFigurePrefub.Skill.Name;
        }
    }

    public void ShowTalentMenu()
    {
    	HideSkillMenu();
        _talentScrollView.SetActive(true);
    }

    public void ShowSkillMenu()
    {
    	HideTalentMenu();
        _skillScrollView.SetActive(true);
    }

    public void HideTalentMenu()
    {
        _talentScrollView.SetActive(false);
    }

    public void HideSkillMenu()
    {
        _skillScrollView.SetActive(false);
    }
}