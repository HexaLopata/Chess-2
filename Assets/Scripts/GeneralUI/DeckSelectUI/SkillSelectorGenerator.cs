using System.Collections.Generic;
using UnityEngine;

public class SkillSelectorGenerator : MonoBehaviour
{
    [SerializeField] private SkillSelector _skillSelectorPrefub;
    [SerializeField] private List<Skill> _skillPool = new List<Skill>();
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private DeckSelectUIManager _uIManager;

    private void Start()
    {
        foreach (var skill in _skillPool)
        {
            var skillSelector = Instantiate(_skillSelectorPrefub, transform);
            skillSelector.Skill = skill;
            skillSelector.DeckManager = _deckManager;
            skillSelector.UIManager = _uIManager;
        }
    }
}
