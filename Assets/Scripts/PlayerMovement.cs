using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CharacterController2D m_CharacterController2D;
    private InputHandler m_InputHandler;

    [Header("Settings")]
    [SerializeField] private float m_FellThroughWorldReset = 200f;
    private int horizontalMovementDirection = 0;
    private bool jump = false;
    private bool collisionJump = false;

    private void Awake()
    {
        m_InputHandler = FindObjectOfType<InputHandler>();
    }

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
    }
}
