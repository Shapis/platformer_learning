using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class NodeLogicHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerNodeMovement m_PlayerNodeMovement;

    [SerializeField] private PopupMenuController m_NodeMenu;

    [Header("Node Menu Stuff")]
    [SerializeField] private TextMeshProUGUI m_LevelNameText;

    [SerializeField] private TextMeshProUGUI m_BestScoreText;

    [SerializeField] private TextMeshProUGUI m_BestTimeText;

    [SerializeField] private Button m_PlayButton;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReached;
        m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDeparted;
        m_PlayerNodeMovement.OnTravelNodeReachedEvent += OnTravelNodeReached;
        //PlayerPrefs.DeleteAll();
        PopulateNodeMap();

        PopulateNodeMenu();
        m_NodeMenu.StartOpenAndMaximized();
    }


    private void OnTravelNodeReached(object sender, GameObject destination)
    {
        MakeThisNodeAccessible(destination.GetComponent<Node>());
    }

    private void OnDestinationNodeReached(object sender, GameObject destination)
    {
        PopulateNodeMenu();
        m_NodeMenu.OpenMenu();
    }

    private void OnDestinationNodeDeparted(object sender, GameObject destination)
    {
        m_NodeMenu.CloseMenu();
    }

    private void PopulateNodeMenu()
    {
        m_LevelNameText.text = m_PlayerNodeMovement.GetCurrentNode().GetNodeName();
        if ((m_PlayerNodeMovement.GetCurrentNode().BestScore != null) && (m_PlayerNodeMovement.GetCurrentNode().BestTime != null))
        {
            m_BestScoreText.text = m_PlayerNodeMovement.GetCurrentNode().BestScore.ToString();
            m_BestTimeText.text = m_PlayerNodeMovement.GetCurrentNode().BestTime.ToString();
        }
        else
        {
            m_BestScoreText.text = "N/A";
            m_BestTimeText.text = "N/A";
        }
        m_PlayButton.onClick.AddListener(() => { SceneHandler.Load(m_PlayerNodeMovement.GetCurrentNode().GetScene()); });

    }

    private void PopulateNodeMap()
    {
        LevelCompletionInfo levelCompletionInfo = new LevelCompletionInfo();
        levelCompletionInfo = levelCompletionInfo.Load();

        var temp = GameObject.FindGameObjectsWithTag("Node");
        Node[] myNodeArray = new Node[temp.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            myNodeArray[i] = temp[i].GetComponent<Node>();
        }

        foreach (var o in myNodeArray)
        {
            o.IsAccessible = false;
        }


        foreach (var o in myNodeArray)
        {
            if (!o.IsTravelNode)
            {
                foreach (var i in levelCompletionInfo.LevelCompletionEntryList)
                {
                    if (o.GetScene() == i.scene)
                    {
                        o.BestScore = i.score;
                        o.BestTime = i.time;
                        MakeAllNearbyAccessible(o);
                    }
                }
            }
        }

        MakeThisNodeAccessible(m_PlayerNodeMovement.GetCurrentNode());
    }

    private static void MakeThisNodeAccessible(Node myNode)
    {
        myNode.IsAccessible = true;
    }

    private static void MakeAllNearbyAccessible(Node myNode)
    {
        if (myNode.m_UpDestination != null)
        {
            myNode.m_UpDestination.GetComponent<Node>().IsAccessible = true;
        }

        if (myNode.m_DownDestination != null)
        {
            myNode.m_DownDestination.GetComponent<Node>().IsAccessible = true;
        }

        if (myNode.m_LeftDestination != null)
        {
            myNode.m_LeftDestination.GetComponent<Node>().IsAccessible = true;
        }

        if (myNode.m_RightDestination != null)
        {
            myNode.m_RightDestination.GetComponent<Node>().IsAccessible = true;
        }
    }
}
