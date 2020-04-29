using System;
using UnityEngine.SceneManagement;

public static class SceneHandler
{

    public enum Scene
    {
        Level01,
        Level02,
        Level03,
        MainMenu,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void Load(string currentScene)
    {
        SceneManager.LoadScene(currentScene);
    }

    public static void ReloadCurrentScene()
    {
        SceneHandler.Load(GetSceneEnum());

    }

    public static Scene GetSceneEnum()
    {
        Scene sceneEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneEnum);

        return sceneEnum;
    }
}
