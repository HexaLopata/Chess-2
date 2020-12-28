using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public void SetSkill(FigureData figure, Skill skill)
    {
        figure.BattleFieldFigurePrefub.Skill = skill;
    }

    public void SetTalent(FigureData figure, Talent talent)
    {
        figure.BattleFieldFigurePrefub.Talent = talent;
    }
}