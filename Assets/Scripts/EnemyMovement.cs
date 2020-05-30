using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController2D controller;

    private float horizontalMove = 0f;

    [SerializeField]
    private float runSpeed = 40f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private BasicEnemyAI myAI;

    [SerializeField]
    public int m_ScoreValue = 1;

    private bool jump = false;

    private void Awake()
    {
    }

    private void Update()
    {
        //myAI.m

        horizontalMove = myAI.CurrentDirection * runSpeed;

        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump = true;
        //    animator.SetBool("IsJumping", true);
        //    OnAirbourne();
        //}
    }

    public void Onlanding()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("isAirbourne", false);
        //Debug.Log("OnLandingEventCallback");
    }

    public void OnAirbourne()
    {
        animator.SetBool("isAirbourne", true);
        //Debug.Log("OnAirbourneEventCallback");
    }

    private void FixedUpdate()
    {
        //controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        //Debug.Log(gameObject + ": is jumping? " + jump);
    }
}
