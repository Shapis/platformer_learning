using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{




    public void ResumeGame()
    {
        GameHandler.Resume();
    }


    public void ExitToMainMenu()
    {
        SceneHandler.Load(SceneHandler.Scene.MainMenu);
    }
}
