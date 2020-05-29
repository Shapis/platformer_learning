using System;
using System.Collections;
using UnityEngine;

public class PlayerNodeAnimations : MonoBehaviour
{

    [SerializeField] private Animator m_Animator;

    [SerializeField] private PlayerNodeMovement m_PlayerNodeMovement;
    private Vector3 previousPosition;
    private bool isFacingRight = true;

    private bool isMovingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDeparted;
        m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReached;
        m_PlayerNodeMovement.OnTravelNodeDepartedEvent += OnTravelNodeDeparted;
    }



    private void OnDestinationNodeReached(object sender, GameObject e)
    {
        m_Animator.SetFloat("speed", 0);
    }

    private void OnDestinationNodeDeparted(object sender, GameObject e)
    {
        m_Animator.SetFloat("speed", 1);
        previousPosition = e.transform.position;
        StopAllCoroutines();
        StartCoroutine("DirectionCheckingCoroutine");
    }

    private void OnTravelNodeDeparted(object sender, GameObject e)
    {
        previousPosition = e.transform.position;
        StopAllCoroutines();
        StartCoroutine("DirectionCheckingCoroutine");
    }

    IEnumerator DirectionCheckingCoroutine()
    {
        yield return new WaitForSeconds(1 / 144f);

        if (gameObject.transform.position.x > previousPosition.x)
        {
            isMovingRight = true;
        }
        else if (gameObject.transform.position.x < previousPosition.x)
        {
            isMovingRight = false;
        }

        if (isMovingRight != isFacingRight)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
