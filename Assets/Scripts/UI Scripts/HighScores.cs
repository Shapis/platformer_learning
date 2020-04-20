using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores
{


    public List<HighScoreEntry> highScoreEntryList;






    public HighScores Load()
    {
        HighScores myTempHighScores;
        myTempHighScores = SaveHandler<HighScores>.Load(SaveHandler<HighScores>.SaveFileName.highScoreTable);


        if (myTempHighScores == null)
        {
            myTempHighScores = new HighScores();
            myTempHighScores.highScoreEntryList = new List<HighScores.HighScoreEntry>()
            {

                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
                new HighScores.HighScoreEntry {score = 0 },
            };

            SaveHandler<HighScores>.Save(myTempHighScores, SaveHandler<HighScores>.SaveFileName.highScoreTable);

        }

        return myTempHighScores;
    }


    public void SortAndTrim(HighScores myTempHighScores)
    {
        for (int i = 0; i < myTempHighScores.highScoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < myTempHighScores.highScoreEntryList.Count; j++)
            {
                if (myTempHighScores.highScoreEntryList[j].score > myTempHighScores.highScoreEntryList[i].score)
                {
                    // Swap
                    HighScores.HighScoreEntry tmp = myTempHighScores.highScoreEntryList[i];
                    myTempHighScores.highScoreEntryList[i] = myTempHighScores.highScoreEntryList[j];
                    myTempHighScores.highScoreEntryList[j] = tmp;
                }
            }
        }

        //// After sorting only keep the top9
        if (myTempHighScores.highScoreEntryList.Count > 9)
        {

            myTempHighScores.highScoreEntryList.RemoveRange(9, myTempHighScores.highScoreEntryList.Count - 9);



            SaveHandler<HighScores>.Save(myTempHighScores, SaveHandler<HighScores>.SaveFileName.highScoreTable);

        }
    }


    public void AddHighScoreEntry(int score)
    {

        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score };

        HighScores highScores = new HighScores();

        highScores = highScores.Load();

        highScores.highScoreEntryList.Add(highScoreEntry);

        highScores.SortAndTrim(highScores);

        SaveHandler<HighScores>.Save(highScores, SaveHandler<HighScores>.SaveFileName.highScoreTable);


    }




    [System.Serializable]
    public class HighScoreEntry
    {
        public int score;


    }

}
