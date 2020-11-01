using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoCore : MonoBehaviour
{
    private Core _core;

    public void Awake()
    {
        _core = new Core(this);
    }

    public void Start()
    {
        var loadOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        Scene menuScene = SceneManager.GetSceneAt(1);
        loadOperation.allowSceneActivation = true;
        loadOperation.completed += (operation) => SceneManager.SetActiveScene(menuScene);
    }
}