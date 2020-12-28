using System.Collections.Generic;
using UnityEngine;

public class TalentSelectorGenerator : MonoBehaviour
{
    [SerializeField] private TalentSelector _talentSelectorPrefub;
    [SerializeField] private List<Talent> _talentPool = new List<Talent>();
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private DeckSelectUIManager _uIManager;

    private void Start()
    {
        foreach(var talent in _talentPool)
        {
            var talentSelector = Instantiate(_talentSelectorPrefub, transform);
            talentSelector.Talent = talent;
            talentSelector.DeckManager = _deckManager;
            talentSelector.UIManager = _uIManager;
        }
    }
}
