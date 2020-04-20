using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SceneHandler.Load(SceneManager.GetActiveScene().name);

    }

    //public static string CurrentSceneName()
    //{



    //    //return this.Scene;
    //}


}
