using UnityEngine;

/// <summary>
/// Предоставляет метод для определения игрового режима
/// </summary>
public class GameModeSetter : MonoBehaviour
{
    /// <summary>
    /// 0 - GameMode.Normal, else GameMode.New
    /// </summary>
    /// <param name="gameMode"></param>
    public void SetGameMode(int gameMode)
    {
        if (gameMode == 0)
            Core.GameMode = GameMode.Normal;
        else
            Core.GameMode = GameMode.New;
    }
}