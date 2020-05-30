using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour, ICharacterEvents
{
    [SerializeField] Animator m_Animator;
    [SerializeField] CharacterController2D m_CharacterController2D;

    private bool isFacingRight = true;

    private void Start()
    {
        m_CharacterController2D.OnAirbourneEvent += OnAirbourne;
        m_CharacterController2D.OnLandingEvent += OnLanding;
        m_CharacterController2D.OnFallingEvent += OnFalling;
        //m_CharacterController2D.OnJumpEvent += OnJump;
        m_CharacterController2D.OnHorizontalMovementChangesEvent += OnHorizontalMovementChanges;
    }

    public void OnAirbourne(object sender, EventArgs e)
    {
        m_Animator.SetBool("isAirbourne", true);
    }

    public void OnLanding(object sender, EventArgs e)
    {
        m_Animator.SetBool("isAirbourne", false);
    }

    public void OnFalling(object sender, EventArgs e)
    {
        m_Animator.SetBool("isAirbourne", true);
    }

    public void OnJump(object sender, EventArgs e)
    {
    }

    public void OnHorizontalMovementChanges(object sender, int movementDirection)
    {
        bool isMoving = false;

        if (movementDirection != 0)
        {
            isMoving = true;
        }
        m_Animator.SetBool("isMoving", isMoving);

        Flip(movementDirection);
    }


    private void Flip(int movementDirection)
    {
        if (movementDirection > 0 && !isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = !isFacingRight;
        }
        else if (movementDirection < 0 && isFacingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            isFacingRight = !isFacingRight;
        }
    }
}
