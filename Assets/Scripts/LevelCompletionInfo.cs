using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletionInfo
{

    public List<LevelCompletionEntry> LevelCompletionEntryList;


    public LevelCompletionInfo Load()
    {
        LevelCompletionInfo myTempLevelCompletionInfo;
        myTempLevelCompletionInfo = SaveHandler<LevelCompletionInfo>.Load(SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);


        if (myTempLevelCompletionInfo == null)
        {
            SaveHandler<LevelCompletionInfo>.Save(myTempLevelCompletionInfo, SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);

        }

        return myTempLevelCompletionInfo;
    }

    public void AddLevelCompletionInfo(SceneHandler.Scene scene, int score, float time)
    {

        LevelCompletionEntry levelCompletionEntry = new LevelCompletionEntry { scene = scene, score = score, time = time };

        LevelCompletionInfo levelCompletionInfo = new LevelCompletionInfo();

        levelCompletionInfo = levelCompletionInfo.Load();

        levelCompletionInfo.LevelCompletionEntryList.Add(levelCompletionEntry);

        // highScores.SortAndTrim(highScores);

        SaveHandler<LevelCompletionInfo>.Save(levelCompletionInfo, SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);


    }

    public class LevelCompletionEntry
    {
        public SceneHandler.Scene scene;
        public int score;
        public float time;
    }

}
