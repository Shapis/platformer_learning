using System.Collections.Generic;
public class LevelCompletionInfo
{
    public List<LevelCompletionEntry> LevelCompletionEntryList = new List<LevelCompletionEntry>();

    public LevelCompletionInfo Load()
    {
        LevelCompletionInfo myTempLevelCompletionInfo;
        myTempLevelCompletionInfo = SaveHandler<LevelCompletionInfo>.Load(SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);

        if (myTempLevelCompletionInfo == null)
        {
            myTempLevelCompletionInfo = new LevelCompletionInfo();
            SaveHandler<LevelCompletionInfo>.Save(myTempLevelCompletionInfo, SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);
        }
        return myTempLevelCompletionInfo;
    }

    public void AddLevelCompletionInfo(SceneHandler.Scene scene, int score, float time)
    {
        bool sceneExists = false;
        LevelCompletionEntry levelCompletionEntry = new LevelCompletionEntry { scene = scene, score = score, time = time };
        LevelCompletionInfo levelCompletionInfo = new LevelCompletionInfo();
        levelCompletionInfo = levelCompletionInfo.Load();
        for (int i = 0; i < levelCompletionInfo.LevelCompletionEntryList.Count; i++)
        {
            if (levelCompletionInfo.LevelCompletionEntryList[i].scene == scene)
            {
                sceneExists = true;
                if (levelCompletionInfo.LevelCompletionEntryList[i].score < score)
                {
                    levelCompletionInfo.LevelCompletionEntryList[i] = levelCompletionEntry;

                }
            }
        }

        if (!sceneExists)
        {
            levelCompletionInfo.LevelCompletionEntryList.Add(levelCompletionEntry);
        }
        SaveHandler<LevelCompletionInfo>.Save(levelCompletionInfo, SaveHandler<LevelCompletionInfo>.SaveFileName.levelCompletionInfo);
    }

    [System.Serializable]
    public class LevelCompletionEntry
    {
        public SceneHandler.Scene scene;
        public int score;
        public float time;
    }
}
