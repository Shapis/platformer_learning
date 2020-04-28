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

    public void AddLevelCompletionInfo(int score)
    {

        LevelCompletionEntry levelCompletionEntry = new LevelCompletionEntry { score = score };

        HighScores highScores = new HighScores();

        highScores = highScores.Load();

        highScores.highScoreEntryList.Add(highScoreEntry);

        highScores.SortAndTrim(highScores);

        SaveHandler<HighScores>.Save(highScores, SaveHandler<HighScores>.SaveFileName.highScoreTable);


    }

    public class LevelCompletionEntry
    {
        public int score;

        public float time;
    }

}
