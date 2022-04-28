using System;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public enum Scene
    {
        Level01,
        Level02,
        Level03,
        Level04,
        Level05,
        Level06,
        Level07,
        Level08,
        Level09,
        Level10,
        Level11,
        Level12,
        Level13,
        Level14,
        Level15,
        Level16,
        Level17,
        Level18,
        Level19,
        Level20,
        MainMenu,
        OverworldMap,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public static void ReloadCurrentScene()
    {
        SceneHandler.Load(GetActiveSceneEnum());

    }

    public static Scene GetActiveSceneEnum()
    {
        Scene sceneEnum;
        Enum.TryParse(SceneManager.GetActiveScene().name, out sceneEnum);

        return sceneEnum;
    }

    public static string GetActiveSceneString()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static bool DoesSceneExist(string myScene)
    {
        return Enum.IsDefined(typeof(Scene), myScene);
    }

    public static Scene StringToScene(string myScene)
    {
        Scene sceneEnum;
        Enum.TryParse(myScene, out sceneEnum);
        return sceneEnum;
    }
}
