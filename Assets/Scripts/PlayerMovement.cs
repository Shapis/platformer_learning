using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, ICharacterEvents, IDraggableEvents
{
    [SerializeField] private CharacterController2D controller;

    private float horizontalMove = 0f;

    [SerializeField] private float runSpeed = 40f;

    [SerializeField] private Animator animator;

    [SerializeField] private float m_FellThroughWorldReset = 200f;

    private bool jump = false;

    private bool collisionJump = false;

    private bool isCasting;

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (!isCasting)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (gameObject.transform.position.y <= m_FellThroughWorldReset)
        {
            //Debug.Log(SceneHandler.CurrentSceneName);
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Died();
            Time.timeScale = 1;
        }
    }

    private void Jump()
    {
        jump = true;
        animator.SetBool("isJumping", true);
        //OnAirbourne();
    }

    public void CollisionJump()
    {
        collisionJump = true;
        animator.SetBool("isJumping", true);
        //OnAirbourne();
    }
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isAirbourne", false);
        //Debug.Log("OnLandingEventCallback");
    }

    public void OnAirbourne()
    {
        animator.SetBool("isAirbourne", true);
        //Debug.Log("OnAirbourneEventCallback");
    }

    public void OnFalling()
    {
    }

    public void OnCrouching()
    {
    }

    public void OnDraggingBegins()
    {
        isCasting = true;
        animator.SetFloat("speed", 0);
    }

    public void OnDraggingEnds()
    {
        isCasting = false;
    }

    private void FixedUpdate()
    {
        if (!isCasting)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, collisionJump);
            jump = false;
            collisionJump = false;
        }
        else
        {
            controller.Casting();
            jump = false;
            collisionJump = false;
        }
    }
}
