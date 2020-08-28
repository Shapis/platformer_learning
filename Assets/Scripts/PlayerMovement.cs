using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CharacterController2D m_CharacterController2D;
    private InputHandler m_InputHandler;

    [Header("Settings")]
    private int horizontalMovementDirection = 0;
    private bool isJumpKeyPressed = false;

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
        m_InputHandler.OnVerticalUpPressedEvent += OnVerticalUpPressed;
        m_InputHandler.OnVerticalUpUnpressedEvent += OnVerticalUpUnpressed;

        m_InputHandler.OnJumpPressedEvent += OnJumpPressed;
        m_InputHandler.OnJumpUnpressedEvent += OnJumpUnpressed;
    }

    private void OnVerticalUpUnpressed(object sender, EventArgs e)
    {
        isJumpKeyPressed = false;
    }

    private void OnJumpUnpressed(object sender, EventArgs e)
    {
        isJumpKeyPressed = false;
    }

    private void OnVerticalUpPressed(object sender, EventArgs e)
    {
        isJumpKeyPressed = true;
    }

    private void OnJumpPressed(object sender, EventArgs e)
    {
        isJumpKeyPressed = true;
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
        m_CharacterController2D.Move(horizontalMovementDirection, isJumpKeyPressed);
    }
}
