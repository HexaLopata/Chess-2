using UnityEngine;

public class FigureFactory : MonoBehaviour
{
    [SerializeField] protected MainFieldFigure _mainFieldFigurePrefub;
    [SerializeField] protected BattleFieldFigure _battleFieldFigurePrefub;

    public FigureData GetFigure(Team team, Skill skill)
    {
        var battleFieldFigurePrefub = Instantiate(_battleFieldFigurePrefub);
        battleFieldFigurePrefub.Skill = skill;
        return new FigureData(_mainFieldFigurePrefub, battleFieldFigurePrefub, team);
    }
}