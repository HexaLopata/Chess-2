public class Core
{
    private MonoCore _monoCore;
    
    public static PlayerData firstPlayerData;
    public static PlayerData secondPlayerData;
    public static CurrentLocation currentLocation;

    public Core(MonoCore monoCore)
    {
        _monoCore = monoCore;
        firstPlayerData = new PlayerData();
        secondPlayerData = new PlayerData();
    }
}
