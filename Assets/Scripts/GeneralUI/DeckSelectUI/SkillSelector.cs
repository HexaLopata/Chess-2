using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelector : MonoBehaviour, IPointerClickHandler
{
    public Skill Skill { get { return _skill; } set { _skill = value; UpdateSkillText(); } }
    public DeckManager DeckManager { get { return _deckManager; } set { _deckManager = value; } }
    public DeckSelectUIManager UIManager { get { return _uIManager; } set { _uIManager = value; } }

    [SerializeField] private DeckSelectUIManager _uIManager;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private Skill _skill;

    private Chess2Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<Chess2Text>();
    }

    private void UpdateSkillText()
    {
        if (_text != null)
            _text.Text = _skill.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _deckManager.SetSkill(_uIManager.CurrentFigure, _skill);
        _uIManager.UpdateInfo();
        _uIManager.HideSkillMenu();
    }
}
