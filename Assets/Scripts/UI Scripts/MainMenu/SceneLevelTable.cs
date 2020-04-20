using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLevelTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;

    //[SerializeField] Scene[] myScene = new Scene[0];

    //private List<Transform> SceneLevelEntryTransformList;

    private List<SceneLevelEntry> sceneLevelEntryList;

    private List<SceneHandler.Scene> scenesList;







    private void Awake()
    {
        entryContainer = GameObject.Find("Container_SceneLevelEntry").transform;
        entryTemplate = GameObject.Find("Template_SceneLevelEntry").transform;

        entryTemplate.gameObject.SetActive(false);



        scenesList = new List<SceneHandler.Scene>();
        scenesList.Add(SceneHandler.Scene.Level01);
        scenesList.Add(SceneHandler.Scene.Level02);
        scenesList.Add(SceneHandler.Scene.Level03);
        scenesList.Add(SceneHandler.Scene.Level01);
        scenesList.Add(SceneHandler.Scene.Level02);
        scenesList.Add(SceneHandler.Scene.Level01);
        scenesList.Add(SceneHandler.Scene.Level02);
        scenesList.Add(SceneHandler.Scene.Level03);
        scenesList.Add(SceneHandler.Scene.Level01);

        sceneLevelEntryList = new List<SceneLevelEntry>();

        for (int i = 0; i < scenesList.Count; i++)
        {
            sceneLevelEntryList.Add(new SceneLevelEntry { highScore = i, bestTime = i * 5, scene = scenesList[i] });
        }


        InstantiateSceneLevelEntryTable(entryContainer, sceneLevelEntryList);










    }


    private void InstantiateSceneLevelEntryTable(Transform container, List<SceneLevelEntry> sceneLevelEntryList)
    {
        // float myMargin = 25f;
        // float templateHeight = 250f + myMargin;
        // float templateWidth = 300f + myMargin;


        for (int i = 0; i < sceneLevelEntryList.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, container);
            // RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            // entryRectTransform.anchoredPosition = new Vector2(-300f - myMargin + templateWidth * i - ((1000 - myMargin) * (i / 3)), 200 - templateHeight * (i / 3));

            int temp = i;

            entryTransform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => { SceneHandler.Load(sceneLevelEntryList[temp].scene); });

            entryTransform.gameObject.SetActive(true);



        }






        // string rankString;

        // int rank = transformList.Count + 1;

        // switch (rank)
        // {
        //     default: rankString = rank + "TH"; break;

        //     case 1: rankString = "1ST"; break;
        //     case 2: rankString = "2ND"; break;
        //     case 3: rankString = "3RD"; break;

        // }

        // entryTransform.Find("PositionText").GetComponent<TextMeshProUGUI>().text = rankString;

        //int score = highScoreEntry.score;
        //entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();

        //transformList.Add(entryTransform);

    }

    private void AddSceneLevelEntry(int score, float time, SceneHandler.Scene myScene)
    {


        // // create and initialize SceneLevelEntry
        SceneLevelEntry sceneLevelEntry = new SceneLevelEntry { highScore = score, bestTime = time, scene = myScene };


        // // Load saved HighScores
        // HighScores highScores = SaveHandler<HighScores>.Load(SaveHandler<HighScores>.SaveFileName.highScoreTable);





        // // Add new entry to sceneLevelEntryList
        sceneLevelEntryList.Add(sceneLevelEntry);



        // // Save updated highscores

        // SaveHandler<HighScores>.Save(highScores, SaveHandler<HighScores>.SaveFileName.highScoreTable);
        // //string json = JsonUtility.ToJson(highScores);
        // //PlayerPrefs.SetString("highScoreTable", json);
        // //PlayerPrefs.Save();


    }

    //[System.Serializable]
    public struct SceneLevelEntry
    {
        public int highScore;

        public float bestTime;
        public SceneHandler.Scene scene;


    }
}
