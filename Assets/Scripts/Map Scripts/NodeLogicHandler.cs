using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeLogicHandler : MonoBehaviour {
    [Header ("Dependencies")]
    [SerializeField] private PlayerNodeMovement m_PlayerNodeMovement;

    [SerializeField] private PopupMenuController m_NodeMenu;

    [Header ("Node Menu Stuff")]
    [SerializeField] private TextMeshProUGUI m_LevelNameText;

    [SerializeField] private TextMeshProUGUI m_BestScoreText;

    [SerializeField] private TextMeshProUGUI m_BestTimeText;

    [SerializeField] private Button m_PlayButton;

    Node[] nodeArray;

    // Start is called before the first frame update
    void Start () {
        m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReached;
        m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDeparted;
        m_PlayerNodeMovement.OnTravelNodeReachedEvent += OnTravelNodeReached;

        GameHandler.Resume (); // Unpauses the game, in case it's reaching the WorldMap after ending a level and pausing it.

        PopulateNodeMap ();
        LoadPlayerCurrentNode ();
        PopulateNodeMenu ();
        m_NodeMenu.StartOpenAndMaximized ();
    }

    private void OnTravelNodeReached (object sender, GameObject destination) {
        //MakeThisNodeAccessible(destination.GetComponent<Node>());
        //SaveAccessibleTravelNodes();
    }

    private void OnDestinationNodeReached (object sender, GameObject destination) {
        PopulateNodeMenu ();
        m_NodeMenu.OpenMenu ();
        SavePlayerCurrentNode ();
        //LoadPlayerCurrentNode();
    }

    private void OnDestinationNodeDeparted (object sender, GameObject destination) {
        m_NodeMenu.CloseMenu ();
    }

    private void PopulateNodeMenu () {
        m_LevelNameText.text = m_PlayerNodeMovement.GetCurrentNode ().GetNodeName ();
        if ((m_PlayerNodeMovement.GetCurrentNode ().BestScore != null) && (m_PlayerNodeMovement.GetCurrentNode ().BestTime != null)) {
            m_BestScoreText.text = m_PlayerNodeMovement.GetCurrentNode ().BestScore.ToString ();
            m_BestTimeText.text = String.Format ("{0:0.00}", m_PlayerNodeMovement.GetCurrentNode ().BestTime);
        } else {
            m_BestScoreText.text = "N/A";
            m_BestTimeText.text = "N/A";
        }
        m_PlayButton.onClick.AddListener (() => { SceneHandler.Load (m_PlayerNodeMovement.GetCurrentNode ().GetScene ()); });

    }

    private void LoadPlayerCurrentNode () {
        CurrentNodeInfo myNodeInfo = new CurrentNodeInfo ();
        try {
            myNodeInfo = SaveHandler<CurrentNodeInfo>.Load (SaveHandler<CurrentNodeInfo>.SaveFileName.currentPlayerNode);

            foreach (var o in nodeArray) {
                if (o.transform.position == myNodeInfo.currentNodePosition) {
                    m_PlayerNodeMovement.SetCurrentNode (o);
                }
            }
        } catch (Exception e) {
            Debug.Log ("Failed at loading the player current saved position. Probably because the SavePlayerCurrentNode method hasnt been executed yet.");
            Debug.Log (e);
        }
    }

    private void SavePlayerCurrentNode () {
        CurrentNodeInfo myCurrentNodeInfo = new CurrentNodeInfo ();
        myCurrentNodeInfo.currentNodePosition = m_PlayerNodeMovement.GetCurrentNode ().transform.position;
        SaveHandler<CurrentNodeInfo>.Save (myCurrentNodeInfo, SaveHandler<CurrentNodeInfo>.SaveFileName.currentPlayerNode);
    }

    private void PopulateNodeMap () {
        LevelCompletionInfo levelCompletionInfo = new LevelCompletionInfo ();
        levelCompletionInfo = levelCompletionInfo.Load ();

        var temp = GameObject.FindGameObjectsWithTag ("Node");
        nodeArray = new Node[temp.Length];

        for (int i = 0; i < temp.Length; i++) {
            nodeArray[i] = temp[i].GetComponent<Node> ();
        }

        foreach (var o in nodeArray) {
            o.IsAccessible = false;
        }

        foreach (var o in nodeArray) {
            if (!o.IsTravelNode) {
                foreach (var i in levelCompletionInfo.LevelCompletionEntryList) {
                    if (o.GetScene () == i.scene) {
                        o.BestScore = i.score;
                        o.BestTime = i.time;
                        MakeThisAndAllNearbyAccessible (o);
                    }
                }
            }
        }
    }

    private static void MakeThisAndAllNearbyAccessible (Node myNode) {
        myNode.IsAccessible = true;

        if (myNode.m_UpDestination != null) {
            if (!myNode.m_UpDestination.GetComponent<Node> ().IsTravelNode) {
                myNode.m_UpDestination.GetComponent<Node> ().IsAccessible = true;
            } else {
                if (!myNode.m_UpDestination.GetComponent<Node> ().IsAccessible) {
                    MakeThisAndAllNearbyAccessible (myNode.m_UpDestination.GetComponent<Node> ());
                }
            }
        }

        if (myNode.m_DownDestination != null) {
            if (!myNode.m_DownDestination.GetComponent<Node> ().IsTravelNode) {
                myNode.m_DownDestination.GetComponent<Node> ().IsAccessible = true;
            } else {
                if (!myNode.m_DownDestination.GetComponent<Node> ().IsAccessible) {
                    MakeThisAndAllNearbyAccessible (myNode.m_DownDestination.GetComponent<Node> ());
                }
            }
        }

        if (myNode.m_LeftDestination != null) {
            if (!myNode.m_LeftDestination.GetComponent<Node> ().IsTravelNode) {
                myNode.m_LeftDestination.GetComponent<Node> ().IsAccessible = true;
            } else {
                if (!myNode.m_LeftDestination.GetComponent<Node> ().IsAccessible) {
                    MakeThisAndAllNearbyAccessible (myNode.m_LeftDestination.GetComponent<Node> ());
                }
            }
        }

        if (myNode.m_RightDestination != null) {
            if (!myNode.m_RightDestination.GetComponent<Node> ().IsTravelNode) {
                myNode.m_RightDestination.GetComponent<Node> ().IsAccessible = true;
            } else {
                if (!myNode.m_RightDestination.GetComponent<Node> ().IsAccessible) {
                    MakeThisAndAllNearbyAccessible (myNode.m_RightDestination.GetComponent<Node> ());
                }
            }
        }
    }

    [System.Serializable]
    public struct CurrentNodeInfo {
        public Vector3 currentNodePosition; // I'd have prefered to save the actual <Node> instead of the position, but apparentyl monobehaviours cant be serialized.
    }

}