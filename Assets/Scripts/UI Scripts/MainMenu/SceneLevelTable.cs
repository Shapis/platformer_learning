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

    [SerializeField] private TextMeshProUGUI m_LevelName;
    [SerializeField] private TextMeshProUGUI m_HighestScore;




    //[SerializeField] Scene[] myScene = new Scene[0];

    //private List<Transform> SceneLevelEntryTransformList;

    private void Awake()
    {
        entryContainer = GameObject.Find("Container_SceneLevelEntry").transform;
        entryTemplate = GameObject.Find("Template_SceneLevelEntry").transform;

        entryTemplate.gameObject.SetActive(false);



        LevelCompletionInfo levelCompletionInfo = new LevelCompletionInfo();
        levelCompletionInfo = levelCompletionInfo.Load(); ;



        InstantiateSceneLevelEntryTable(entryContainer, levelCompletionInfo.LevelCompletionEntryList);
    }


    private void InstantiateSceneLevelEntryTable(Transform container, List<LevelCompletionInfo.LevelCompletionEntry> levelCompletionEntryList)
    {
        // float myMargin = 25f;
        // float templateHeight = 250f + myMargin;
        // float templateWidth = 300f + myMargin;

        for (int i = 0; i < 90; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, container);
            // RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            // entryRectTransform.anchoredPosition = new Vector2(-300f - myMargin + templateWidth * i - ((1000 - myMargin) * (i / 3)), 200 - templateHeight * (i / 3));
            SceneHandler.Scene? myScene;
            switch (i)
            {
                case 0: myScene = SceneHandler.Scene.Level01; break;
                case 1: myScene = SceneHandler.Scene.Level02; break;
                case 2: myScene = SceneHandler.Scene.Level03; break;
                default: myScene = null; break;
            }


            foreach (var o in levelCompletionEntryList)
            {
                if (o.scene == myScene)
                {
                    entryTransform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => { SceneHandler.Load(o.scene); });
                    entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = o.scene.ToString();
                    entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = o.score.ToString();
                    break;
                }
                else
                {
                    entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myScene.ToString();
                    entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
                }

            }

            if (myScene == null)
            {
                //Debug.Log(i);
                entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Under Construction!";
                entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
            }

            // // if (i < levelCompletionEntryList.Count)
            // // {
            // //     int temp = i;

            // //     entryTransform.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => { SceneHandler.Load(levelCompletionEntryList[temp].scene); });

            // //     entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelCompletionEntryList[i].scene.ToString();
            // //     entryTransform.gameObject.GetComponentInChildren<Button>().gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = levelCompletionEntryList[i].score.ToString();

            // // }
            entryTransform.gameObject.SetActive(true);
        }
    }
}
