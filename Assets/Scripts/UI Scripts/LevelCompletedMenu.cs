using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedMenu : MonoBehaviour
{
    [SerializeField] GameObject myPlayer;
    public void Proceed()
    {
        SceneHandler.Load(myPlayer.GetComponent<GemGrabber>().MyNextScene);

    }
    public void ExitToMainMenu()
    {
        SceneHandler.Load(SceneHandler.Scene.MainMenu);
    }
}
