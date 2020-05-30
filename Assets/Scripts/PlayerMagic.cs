using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public GameObject ObjectBeingDragged { get; set; }

    public float m_DraggingRange { get; set; } = 5f;

    private GameObject myTargetGameObjectHitPoint;

    RaycastHit2D[] myMultiRaycastInfo;

    [SerializeField] LightningScript m_LightningScript;

    [SerializeField] OutlineHighlightScript m_OutlineHighlightScript;




    void Awake()
    {
        myTargetGameObjectHitPoint = new GameObject();
        myTargetGameObjectHitPoint.transform.name = "lightningEffectHitPoint";
        myTargetGameObjectHitPoint.transform.parent = GameObject.Find("EffectsContainer").transform;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (ObjectBeingDragged != null)
        {

            RaycastToObjectBeingDragged();

            //myTargetGameObjectHitPoint.transform.position = ObjectBeingDragged.transform.position;
            for (int i = 0; i < myMultiRaycastInfo.Length; i++)
            {
                if (myMultiRaycastInfo[i].transform == ObjectBeingDragged.transform)
                {
                    myTargetGameObjectHitPoint.transform.position = myMultiRaycastInfo[i].point;

                }
            }


            //Debug.Log(ObjectBeingDragged.transform.name);
        }

        // RaycastToObjectBeingDragged();

        // foreach (var o in myRaycastArray)
        // {
        //     if (o == ObjectBeingDragged)
        //     {
        //         Debug.Log("hit target");
        //         myTargetGameObjectHitPoint.GetComponent<Transform>().position = o.point;

        //     }
        // }


    }

    public void OnDraggingBegins()
    {
        //
        myTargetGameObjectHitPoint.transform.position = ObjectBeingDragged.transform.position; // to make sure when it starts the lightning animation, it picks the currently being dragged object.
        //
        m_LightningScript.Begin(gameObject.transform, myTargetGameObjectHitPoint.GetComponent<Transform>());
        //m_OutlineHighlightScript.Begin(ObjectBeingDragged);

    }

    public void OnDraggingEnds()
    {
        m_LightningScript.End();
        //m_OutlineHighlightScript.End();

    }

    private void RaycastToObjectBeingDragged()
    {
        myMultiRaycastInfo = Physics2D.RaycastAll(gameObject.transform.position, ObjectBeingDragged.transform.position - gameObject.transform.position);
        System.Array.Sort(myMultiRaycastInfo, (x, y) => x.distance.CompareTo(y.distance));

    }
}
