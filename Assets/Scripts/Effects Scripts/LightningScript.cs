using UnityEngine;

public class LightningScript : MonoBehaviour
{
    [SerializeField] private GameObject m_EffectsContainer;
    [SerializeField] private int m_LightningLines = 5;
    [SerializeField] private float m_LineWidth = 0.03f;
    [SerializeField] private float updateTimer = 0.05f;
    [SerializeField] private Color[] m_PossibleColors;
    [SerializeField] private float randomnessFactor;
    private GameObject[] myLightningLinesArray;
    private Vector3[] myPoints;
    private Transform startingTransform;
    private Transform endingTransform;
    private GeometryHandler myGeometryHandler;
    private bool hasBegun;
    private float timer;
    private float randomness;

    private void Awake()
    {
        myLightningLinesArray = new GameObject[m_LightningLines];
        myPoints = new Vector3[m_LightningLines];

        myGeometryHandler = new GeometryHandler();

        m_EffectsContainer = GameObject.Find("EffectsContainer");

        //myPlayerMagic = gameObject.GetComponent<PlayerMagic>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (hasBegun)
        {
            foreach (var o in myLightningLinesArray)
            {
                CalculatePoints(o.GetComponent<LineRenderer>());
            }
        }
    }

    public void Begin(Transform startingTransform, Transform endingTransform)
    {
        this.startingTransform = startingTransform;
        this.endingTransform = endingTransform;
        hasBegun = true;



        myPoints[0] = startingTransform.position;
        myPoints[4] = endingTransform.position;
        myPoints[2] = myGeometryHandler.GetCenter(myPoints[0], myPoints[4]);
        myPoints[1] = myGeometryHandler.GetCenter(myPoints[0], myPoints[2]);
        myPoints[3] = myGeometryHandler.GetCenter(myPoints[2], myPoints[4]);

        for (int i = 0; i < m_LightningLines; i++)
        {
            myLightningLinesArray[i] = myGeometryHandler.DrawLine(m_EffectsContainer, myPoints, new Vector2(m_LineWidth, m_LineWidth), m_PossibleColors[0]);
            AdjustSortingLayerToTargetSortingLayer(myLightningLinesArray[i].GetComponent<LineRenderer>());
        }
    }

    public void End()
    {
        hasBegun = false;

        foreach (var o in myLightningLinesArray)
        {
            Destroy(o);
        }
    }

    private void CalculatePoints(LineRenderer myLineRenderer)
    {
        timer += Time.deltaTime;

        if (timer > updateTimer)
        {
            timer = 0;
            myLineRenderer.SetPosition(0, startingTransform.position);
            myLineRenderer.SetPosition(4, endingTransform.position);
            myLineRenderer.SetPosition(2, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(0), myLineRenderer.GetPosition(4)));
            myLineRenderer.SetPosition(1, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(0), myLineRenderer.GetPosition(2)));
            myLineRenderer.SetPosition(3, myGeometryHandler.GetCenter(myLineRenderer.GetPosition(2), myLineRenderer.GetPosition(4)));

            float distance = Vector3.Distance(startingTransform.position, endingTransform.position) / myLineRenderer.positionCount;

            randomness = randomnessFactor * distance / (myLineRenderer.positionCount * 2);

            SetRandomness(myLineRenderer);
            ChangeToRandomColor(myLineRenderer);

            AdjustSortingLayerToTargetSortingLayer(myLineRenderer);
        }





        myLineRenderer.SetPosition(0, startingTransform.position);
        myLineRenderer.SetPosition(4, endingTransform.position);
    }

    private void ChangeToRandomColor(LineRenderer myLineRenderer)
    {
        System.Random rnd = new System.Random();
        int myRnd = 0;

        if (!gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<DraggableScript>().LineOfSightLeniencySwitch)
        {
            myRnd = rnd.Next(0, 2);
        }
        else
        {
            myRnd = rnd.Next(3, m_PossibleColors.Length);

        }


        myLineRenderer.material.color = m_PossibleColors[myRnd];


    }

    private void AdjustSortingLayerToTargetSortingLayer(LineRenderer myLineRenderer)
    {
        myLineRenderer.sortingLayerID = gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<SpriteRenderer>().sortingLayerID;
        myLineRenderer.sortingOrder = gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.GetComponent<SpriteRenderer>().sortingOrder;
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
