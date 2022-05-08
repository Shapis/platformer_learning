using UnityEngine;

public class Node : MonoBehaviour
{
    // Stuff for NodePathFinding.cs
    public float f;
    public float g;
    public float h;
    public Node previousPathNode;
    //

    [Header("Node Information")]
    [SerializeField] private string m_NodeName = "Level Name";

    public string GetNodeName()
    {
        return m_NodeName;
    }

    [SerializeField] private SceneHandler.Scene m_Scene;
    [SerializeField] private AudioClipCatalog.MusicName m_Music;

    public AudioClipCatalog.MusicName GetMusic()
    {
        return m_Music;
    }

    public SceneHandler.Scene GetScene()
    {
        return m_Scene;
    }

    public int? BestScore { get; set; } = null; // These are nullables so the nodelogichandler knows they havent been initialized.

    public float? BestTime { get; set; } = null; // These are nullables so the nodelogichandler knows they havent been initialized.

    [Header("Node Destinations")]

    [SerializeField] public GameObject m_UpDestination;
    [SerializeField] public GameObject m_DownDestination;
    [SerializeField] public GameObject m_LeftDestination;
    [SerializeField] public GameObject m_RightDestination;
    [SerializeField] public bool IsTravelNode;
    [SerializeField] public bool IsAccessible = true;


    private void Start()
    {
        //gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
