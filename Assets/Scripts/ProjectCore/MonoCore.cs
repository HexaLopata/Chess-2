using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Загружает первую игровую сцену
/// </summary>
public class MonoCore : MonoBehaviour
{
    private Core _core;

    private void Awake()
    {
        _core = new Core(this);
    }

    private void Start()
    {
        var loadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        Scene menuScene = SceneManager.GetSceneByBuildIndex(1);
        loadOperation.allowSceneActivation = true;
        loadOperation.completed += (operation) => SceneManager.SetActiveScene(menuScene);
    }
}