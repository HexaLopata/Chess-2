#if DEBUG
using UnityEditor;
using UnityEditor.SceneManagement;


[InitializeOnLoad]
public class PlayModeStartSceneSetup
{
    public const int START_SCENE_INDEX = 0;
     
    static PlayModeStartSceneSetup()
    {
        SceneListChanged();
        EditorBuildSettings.sceneListChanged += SceneListChanged;
    }
 
    static void SceneListChanged()
    {
        if (EditorBuildSettings.scenes.Length == 0)
            return;
        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[START_SCENE_INDEX].path);
        EditorSceneManager.playModeStartScene = scene;
    }
}
#endif