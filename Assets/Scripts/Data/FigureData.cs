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
    public BattleFieldFigure BattleFieldFigurePrefub => _battleFieldFigurePrefubPrefub;
    public MainFieldFigure MainFieldFigurePrefub => _mainFieldFigurePrefubPrefub;
    public MainFieldFigure MainFieldFigureInstance { get; set; }

    public Team Team => _team;

    private readonly MainFieldFigure _mainFieldFigurePrefubPrefub;
    private readonly BattleFieldFigure _battleFieldFigurePrefubPrefub;
    private Team _team;
    public int Health { get; set; } = 100;

    public FigureData(MainFieldFigure mainFieldFigurePrefubPrefub, BattleFieldFigure battleFieldFigurePrefubPrefub, Team team)
    {
        _mainFieldFigurePrefubPrefub = mainFieldFigurePrefubPrefub;
        _battleFieldFigurePrefubPrefub = battleFieldFigurePrefubPrefub;
        _team = team;
    }

    public FigureData Clone()
    {
        return new FigureData(_mainFieldFigurePrefubPrefub, _battleFieldFigurePrefubPrefub, Team);
    }
}