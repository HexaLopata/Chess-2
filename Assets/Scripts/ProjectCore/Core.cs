public class Core
{
    private MonoCore _monoCore;
    
    public static PlayerData FirstPlayerData { get; set; }
    public static PlayerData SecondPlayerData { get; set; }
    public static CurrentLocation CurrentLocation { get; set; }
    public static GameMode GameMode { get; set; } = GameMode.Normal;

    public Core(MonoCore monoCore)
    {
        _monoCore = monoCore;
        FirstPlayerData = new PlayerData();
        SecondPlayerData = new PlayerData();
        CurrentLocation = new CurrentLocation();
    }
}
