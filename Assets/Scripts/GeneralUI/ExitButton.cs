using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Реализация скрипта для кнопки выхода из игры
/// </summary>
public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
