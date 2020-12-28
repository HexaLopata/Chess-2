using UnityEngine;
using UnityEngine.EventSystems;

public class TalentSelector : MonoBehaviour, IPointerClickHandler
{
    public Talent Talent { get { return _talent; } set { _talent = value; UpdateTalentText(); } }
    public DeckManager DeckManager { get { return _deckManager; } set { _deckManager = value; } }
    public DeckSelectUIManager UIManager { get { return _uIManager; } set { _uIManager = value; } }

    [SerializeField] private DeckSelectUIManager _uIManager;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private Talent _talent;

    private Chess2Text _text;

    private void Awake()
    {
        _text = GetComponentInChildren<Chess2Text>();
    }

    private void UpdateTalentText()
    {
        if (_text != null)
            _text.Text = _talent.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _deckManager.SetTalent(_uIManager.CurrentFigure, _talent);
        _uIManager.UpdateInfo();
        _uIManager.HideTalentMenu();
    }
}