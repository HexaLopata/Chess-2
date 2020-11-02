using UnityEngine;

public class FigureFactory : MonoBehaviour
{
    [SerializeField] protected MainFieldFigure _mainFieldFigurePrefub;
    [SerializeField] protected BattleFieldFigure _battleFieldFigurePrefub;

    public FigureData GetFigure(Team team)
    {
        return new FigureData(_mainFieldFigurePrefub, _battleFieldFigurePrefub, team);
    }
}