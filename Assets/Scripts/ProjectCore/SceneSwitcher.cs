using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Предоставляет метод для переключения сцен
/// </summary>
public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(int sceneId)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        loadOperation.completed += (operation) => {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneId));
            SceneManager.UnloadSceneAsync(gameObject.scene);
        };
    }
}