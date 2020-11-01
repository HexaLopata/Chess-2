using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScene(int sceneId)
    {
        var loadOperation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        loadOperation.completed += (operation) => {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneId));
            SceneManager.UnloadSceneAsync(gameObject.scene);
        };
    }
}
