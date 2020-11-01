using UnityEngine;

public enum Team
{
    White,
    Black
}

public class FigureData : IFigureDataPrototype
{
    private GameObject _mainFieldFigurePrefub;
    private GameObject _battleFieldFigurePrefub;
    private Team _team;
    private int health = 100;

    public FigureData(GameObject mainFieldFigurePrefub, GameObject battleFieldFigurePrefub, Team team)
    {
        _mainFieldFigurePrefub = mainFieldFigurePrefub;
        _battleFieldFigurePrefub = battleFieldFigurePrefub;
        _team = team;
    }

    FigureData IFigureDataPrototype.Clone()
    {
        return new FigureData(_mainFieldFigurePrefub, _battleFieldFigurePrefub, _team);
    }
}

public interface IFigureDataPrototype
{
    FigureData Clone();
}