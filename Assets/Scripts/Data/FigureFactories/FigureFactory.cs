using UnityEngine;

/// <summary>
/// Фабрика для получения фигуры
/// </summary>
public class FigureFactory : MonoBehaviour
{
    [SerializeField] protected MainFieldFigure _mainFieldFigurePrefub;
    [SerializeField] protected BattleFieldFigure _battleFieldFigurePrefub;

    public FigureData GetFigure(Team team, Skill skill, Talent talent)
    {
        var battleFieldFigurePrefub = Instantiate(_battleFieldFigurePrefub);
        battleFieldFigurePrefub.Skill = skill;
        battleFieldFigurePrefub.Talent = talent;
        return new FigureData(_mainFieldFigurePrefub, battleFieldFigurePrefub, team);
    }
}