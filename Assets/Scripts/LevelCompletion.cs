using UnityEngine;
using System.Linq;
using System;

public class LevelCompletion
{




    public SceneHandler.Scene LevelHasBeenCompleted()
    {


        LevelCompletionInfo myLevelCompletionInfo = new LevelCompletionInfo();
        //PlayerPrefs.DeleteAll();
        myLevelCompletionInfo = myLevelCompletionInfo.Load();

        // Debug.Log(GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalScore);
        // Debug.Log(GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalTime);
        myLevelCompletionInfo.AddLevelCompletionInfo(SceneHandler.GetActiveSceneEnum(),
        GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalScore,
        GameObject.Find("Player").GetComponent<PlayerScoreKeeper>().TotalTime);

        GameHandler.Pause();

        return AddNextSceneToLevelCompletionInfo(myLevelCompletionInfo);






        //Debug.Log(myLevelCompletionInfo.LevelCompletionEntryList.Count);
        //Debug.Log(myLevelCompletionInfo.LevelCompletionEntryList[0].score);

        //myLevelCompletionInfo.AddLevelCompletionInfo(SceneHandler.GetActiveSceneEnum(), 5, 5);
        //Debug.Log("Added level completion info!");

    }

    private SceneHandler.Scene AddNextSceneToLevelCompletionInfo(LevelCompletionInfo myLevelCompletionInfo)
    {
        int result = Int32.Parse(string.Concat(SceneHandler.GetActiveSceneString().ToArray().Reverse().TakeWhile(char.IsNumber).Reverse())); //Gets the int at the end of the scene name string.
        result++; // Adds 1 to that int.
        string myNextLevel = "Level" + String.Format("{0:00}", result); // Converts that into the right format for level names.

        //Checks if the next level exists, if it does, adds it to LevelCompletionInfo.
        if (SceneHandler.DoesSceneExist(myNextLevel))
        {
            SceneHandler.Scene myNextScene = SceneHandler.StringToScene(myNextLevel);
            myLevelCompletionInfo.AddLevelCompletionInfo(myNextScene, 0, 0);
            return myNextScene;
        }
        else
        {
            return SceneHandler.Scene.MainMenu;
        }

    }

}
