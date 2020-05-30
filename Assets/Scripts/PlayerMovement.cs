using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D m_CharacterController2D;
    [SerializeField] private InputHandler m_InputHandler;

    private int horizontalMovementDirection = 0;

    [SerializeField] private float m_FellThroughWorldReset = 200f;

    private bool jump = false;

    private bool collisionJump = false;

    private void Start()
    {
        m_InputHandler.OnHorizontalRightPressedEvent += OnHorizontalRightPressed;
        m_InputHandler.OnHorizontalRightUnpressedEvent += OnHorizontalUnpressed;
        m_InputHandler.OnHorizontalLeftPressedEvent += OnHorizontalLeftPressed;
        m_InputHandler.OnHorizontalLeftUnpressedEvent += OnHorizontalUnpressed;

        m_InputHandler.OnJumpPressedEvent += OnJumpPressed;
    }

    private void OnJumpPressed(object sender, EventArgs e)
    {
        jump = true;
    }

    private void OnHorizontalUnpressed(object sender, EventArgs e)
    {
        horizontalMovementDirection = 0;
    }

    private void OnHorizontalRightPressed(object sender, EventArgs e)
    {
        horizontalMovementDirection = 1;
    }

    private void OnHorizontalLeftPressed(object sender, EventArgs e)
    {
        horizontalMovementDirection = -1;
    }

    private void FixedUpdate()
    {
        m_CharacterController2D.Move(horizontalMovementDirection, jump);
        jump = false;
        PlayerFellThroughWorldFailsafe();
    }

    private void PlayerFellThroughWorldFailsafe()
    {
        if (gameObject.transform.position.y <= m_FellThroughWorldReset)
        {
            //Debug.Log(SceneHandler.CurrentSceneName);
            PlayerDied myPlayerDied = new PlayerDied();
            myPlayerDied.Died();
            Time.timeScale = 1;
        }
    }
}
