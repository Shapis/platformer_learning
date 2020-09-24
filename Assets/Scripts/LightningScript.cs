using UnityEngine;

public class LightningScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject m_EffectsContainer;
    [SerializeField] private int m_LightningLines = 5;
    [SerializeField] private float m_LineWidth = 0.03f;
    [SerializeField] private float updateTimer = 0.05f;
    [SerializeField] private Color[] m_PossibleColors;
    [SerializeField] private float randomnessFactor;
    private GameObject[] myLightningLinesArray;
    private Vector3[] myPoints;
    private Transform originTransform;
    private Transform targetTransform;
    public GameObject ClosestBlockingObject { get; set; }
    private GeometryHandler myGeometryHandler;
    private bool hasBegun;
    private float timer;
    private float randomness;


    private void Start()
    {
        myLightningLinesArray = new GameObject[m_LightningLines];
        myPoints = new Vector3[m_LightningLines];
        myGeometryHandler = new GeometryHandler();
        m_EffectsContainer = GameObject.Find("EffectsContainer");
    }

    private void Update()
    {
        if (hasBegun)
        {
            foreach (var o in myLightningLinesArray)
            {
                CalculatePoints(o.GetComponent<LineRenderer>(), originTransform, TargetHitPoint());
            }
        }
    }

    private Vector2 TargetHitPoint()
    {
        RaycastHit2D[] myRayInfoArray = Physics2D.RaycastAll(originTransform.position, (Vector2)targetTransform.position - (Vector2)originTransform.position);

        foreach (var o in myRayInfoArray)
        {

            if (o.transform.gameObject == targetTransform.gameObject)
            {
                return o.point;
            }
        }

        return Vector2.zero; // this should never happen
    }

    public void Begin(Transform originTransform, Transform targetTransform, GameObject closestBlockingTransform)
    {
        this.originTransform = originTransform;
        this.targetTransform = targetTransform;
        this.ClosestBlockingObject = closestBlockingTransform;
        for (int i = 0; i < m_LightningLines; i++)
        {
            myLightningLinesArray[i] = myGeometryHandler.DrawLine(m_EffectsContainer, myPoints, new Vector2(m_LineWidth, m_LineWidth), ReturnRandomColor());
        }
        hasBegun = true;
    }

    public void End()
    {
        hasBegun = false;

        foreach (var o in myLightningLinesArray)
        {
            Destroy(o);
        }
    }

    private void CalculatePoints(LineRenderer myLineRenderer, Transform origin, Vector2 target)
    {
        // timer += Time.deltaTime;

        // if (timer > updateTimer)
        // {
        timer = 0;
        myLineRenderer.SetPosition(0, origin.position);
        myLineRenderer.SetPosition(4, target);
        myLineRenderer.SetPosition(2, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(0), myLineRenderer.GetPosition(4)));
        myLineRenderer.SetPosition(1, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(0), myLineRenderer.GetPosition(2)));
        myLineRenderer.SetPosition(3, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(2), myLineRenderer.GetPosition(4)));
        float distance = Vector3.Distance(origin.position, target) / myLineRenderer.positionCount;
        randomness = randomnessFactor * distance / (myLineRenderer.positionCount * 2);
        SetRandomness(myLineRenderer);
        myLineRenderer.material.color = ReturnRandomColor();
        AdjustSortingLayerToTargetSortingLayer(myLineRenderer);
        // }
        myLineRenderer.SetPosition(0, origin.position);
        myLineRenderer.SetPosition(4, target);
    }

    private Color ReturnRandomColor()
    {
        if (ClosestBlockingObject == null)
        {
            return m_PossibleColors[Random.Range(0, 2)];
        }
        else
        {
            return m_PossibleColors[Random.Range(3, m_PossibleColors.Length)];
        }
    }

    private void AdjustSortingLayerToTargetSortingLayer(LineRenderer myLineRenderer)
    {
        // myLineRenderer.sortingLayerID = gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<SpriteRenderer>().sortingLayerID;
        // myLineRenderer.sortingOrder = gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void SetRandomness(LineRenderer myLineRenderer)
    {
        for (int i = 0; i < myLightningLinesArray.Length; i++)
        {
            if (i != 0 && i != myLightningLinesArray.Length - 1)
            {
                myLineRenderer.SetPosition(i, myLineRenderer.GetPosition(i) + new Vector3(UnityEngine.Random.Range(-randomness, randomness), UnityEngine.Random.Range(-randomness, randomness), UnityEngine.Random.Range(-randomness, randomness)));
            }
        }
    }
}
