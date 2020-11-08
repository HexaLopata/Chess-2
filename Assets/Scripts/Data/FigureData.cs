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
    public BattleFieldFigure BattleFieldFigurePrefub => _battleFieldFigurePrefub;
    public MainFieldFigure MainFieldFigurePrefub => _mainFieldFigurePrefub;
    public MainFieldFigure MainFieldFigureInstance { get; set; }
    public BattleFieldFigure BattleFieldFigureInstance { get; set; }

    public Team Team => _team;

    private readonly MainFieldFigure _mainFieldFigurePrefub;
    private readonly BattleFieldFigure _battleFieldFigurePrefub;
    private Team _team;
    public int Health { get; set; } = 100;

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