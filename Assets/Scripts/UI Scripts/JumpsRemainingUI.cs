using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpsRemainingUI : MonoBehaviour, ICharacterEvents
{
    [SerializeField]
    private Transform myTransform;


    private GameObject myPlayer;

    [SerializeField]
    private GameObject myJumpChargeContainer;

    private int myNumberOfJumpsLeft;

    [SerializeField]
    private Sprite mySprite;

    private GameObject[] myJumpChargeArray;

    [SerializeField]
    private Vector2 m_JumpChargeSize = new Vector2(0, 0);

    [SerializeField]
    private Vector2 m_JumpChargeOffset = new Vector2(0, 0);

    //[SerializeField] private float m_JumpChargesSpacing = 1;
    private bool myPlayerIsGrounded = false;

    [SerializeField]
    private Vector2 m_JumpChargeAlpha = new Vector2(1f, 1f);


    private void Awake()
    {
        myPlayer = GameObject.Find("Player");
    }

    private void Start()
    {
        myJumpChargeArray = new GameObject[0];

        myJumpChargeContainer.GetComponent<SpriteRenderer>().sprite = null;

        myPlayer.GetComponent<CharacterController2D>().OnLandingEvent.AddListener(OnLanding);
        myPlayer.GetComponent<CharacterController2D>().OnAirbourneEvent.AddListener(OnAirbourne);
        myPlayer.GetComponent<CharacterController2D>().OnFallingEvent.AddListener(OnFalling);
        //myPlayer.GetComponent<CharacterController2D>().OnCrouchingEvent.AddListener(OnCrouching);
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObject.Find("Player").GetComponent<CharacterController2D>().DoubleJumpsRemaining);
        //Debug.Log(GameObject.Find("Player").GetComponent<CharacterController2D>().Grounded);
        myNumberOfJumpsLeft =
            myPlayer.GetComponent<CharacterController2D>().DoubleJumpsRemaining;

        if (myPlayerIsGrounded && myPlayer.GetComponent<CharacterController2D>().DoubleJumpsRemaining <
            myPlayer.GetComponent<CharacterController2D>().NumberOfDoubleJumps + 1)
        {
            myNumberOfJumpsLeft =
                myPlayer
                    .GetComponent<CharacterController2D>()
                    .NumberOfDoubleJumps +
                1;
        }

        int tempArraySize = myJumpChargeArray.Length;

        // If myJumpChargeArray cant fill all the current jump charges, create a new myJumpChargeArray that can.
        TrimArray();

        FillArray();

        //foreach(GameObject o in myJumpChargeArray)
        //{
        //    o.GetComponent<Transform>().position = new Vector3(2f, 2f, 2f);
        //}
        if (myNumberOfJumpsLeft != myJumpChargeArray.Length)
        {
            Debug.Log("Number of jumps left and array size out of sync!");
        }

        for (int i = 0; i < myJumpChargeArray.Length; i++)
        {
            myJumpChargeArray[i].transform.localPosition =
                new Vector2(i *
                    m_JumpChargeOffset.x /
                    ((myTransform.localScale.x) / 10),
                    i *
                    m_JumpChargeOffset.y /
                    ((myTransform.localScale.y) / 10));
            myJumpChargeArray[i].transform.localScale = m_JumpChargeSize;

            if (
                i <=
                myPlayer
                    .GetComponent<CharacterController2D>()
                    .NumberOfDoubleJumps
            )
            {
                Color tempColor = Color.yellow;
                tempColor.a = m_JumpChargeAlpha.x;
                myJumpChargeArray[i].GetComponent<SpriteRenderer>().color =
                    tempColor;
            }
            else
            {
                Color tempColor = Color.green;
                tempColor.a = m_JumpChargeAlpha.y;
                myJumpChargeArray[i].GetComponent<SpriteRenderer>().color =
                    tempColor;
            }
        }

        //foreach (GameObject o in myJumpChargeArray)
        //{
        //    o.transform.localPosition = new Vector2(0.3f, 0.3f);
        //}
    }

    private GameObject CreateNewJumpChargeObject()
    {
        GameObject myGameObject = new GameObject("JumpCharge");
        SpriteRenderer renderer = myGameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = mySprite;
        myGameObject.transform.SetParent(myJumpChargeContainer.transform);
        myGameObject.transform.localPosition = new Vector3(0, 0, 0);

        myGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

        //myGameObject.GetComponent<Transform>().position = new Vector3(2, 2, 2);
        return myGameObject;
    }

    private void TrimArray()
    {
        if (myJumpChargeArray.Length < myNumberOfJumpsLeft)
        {
            GameObject[] tempArray = new GameObject[myNumberOfJumpsLeft];

            for (int i = 0; i < myJumpChargeArray.Length; i++)
            {
                tempArray[i] = myJumpChargeArray[i];
            }

            myJumpChargeArray = new GameObject[myNumberOfJumpsLeft];

            for (int i = 0; i < tempArray.Length; i++)
            {
                myJumpChargeArray[i] = tempArray[i];
            }
        }
        else if (myJumpChargeArray.Length > myNumberOfJumpsLeft)
        {
            GameObject[] tempArray = new GameObject[myNumberOfJumpsLeft];

            for (int i = 0; i < myNumberOfJumpsLeft; i++)
            {
                tempArray[i] = myJumpChargeArray[i];
            }

            for (
                int i = 0;
                i < myJumpChargeArray.Length - myNumberOfJumpsLeft;
                i++
            )
            {
                Destroy(myJumpChargeArray[myJumpChargeArray.Length - 1 - i]);
            }

            myJumpChargeArray = tempArray;
        }
    }

    private void FillArray()
    {
        for (int i = 0; i < myJumpChargeArray.Length; i++)
        {
            if (myJumpChargeArray[i] == null)
            {
                myJumpChargeArray[i] = CreateNewJumpChargeObject();
            }
        }
    }

    public void OnLanding()
    {
        myPlayerIsGrounded = true;
    }

    public void OnAirbourne()
    {
        myPlayerIsGrounded = false;
    }

    public void OnFalling()
    {

    }

    public void OnCrouching()
    {

    }

}



