using UnityEngine;

public class FigureFactory : MonoBehaviour
{
    [SerializeField] protected GameObject _mainFieldFigurePrefub;
    [SerializeField] protected GameObject _battleFieldFigurePrefub;

    public FigureData GetFigure(Team team)
    {
        return new FigureData(_mainFieldFigurePrefub, _battleFieldFigurePrefub, team);
    }
}