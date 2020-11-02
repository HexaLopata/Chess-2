using UnityEngine;

public enum Team
{
    White,
    Black
}

public interface IFigureDataPrototype
{
    FigureData Clone();
}

public class FigureData : IFigureDataPrototype
{
    public BattleFieldFigure BattleFieldFigure => _battleFieldFigurePrefub;
    public MainFieldFigure MainFieldFigure => _mainFieldFigurePrefub;

    public Team Team => _team;

    private MainFieldFigure _mainFieldFigurePrefub;
    private BattleFieldFigure _battleFieldFigurePrefub;
    private Team _team;
    private int health = 100;

    public FigureData(MainFieldFigure mainFieldFigurePrefub, BattleFieldFigure battleFieldFigurePrefub, Team team)
    {
        _mainFieldFigurePrefub = mainFieldFigurePrefub;
        _battleFieldFigurePrefub = battleFieldFigurePrefub;
        _team = team;
    }

    public FigureData Clone()
    {
        return new FigureData(_mainFieldFigurePrefub, _battleFieldFigurePrefub, Team);
    }
}