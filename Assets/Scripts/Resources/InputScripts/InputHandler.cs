using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private bool m_DebugLogsEnabled = false;

    // Cancel events //////////////////////////////////////////////////////
    public event EventHandler OnCancelPressedEvent;
    public event EventHandler OnCancelUnpressedEvent;
    ///////////////////////////////////////////////////////////////////////

    // Horizontal events //////////////////////////////////////////////////
    public event EventHandler OnHorizontalLeftPressedEvent;
    public event EventHandler OnHorizontalLeftUnpressedEvent;
    public event EventHandler OnHorizontalRightPressedEvent;
    public event EventHandler OnHorizontalRightUnpressedEvent;
    private int horizontalDirection;
    private bool horizontalBusy;
    ///////////////////////////////////////////////////////////////////////

    // Vertical Events ////////////////////////////////////////////////////
    public event EventHandler OnVerticalUpPressedEvent;
    public event EventHandler OnVerticalUpUnpressedEvent;
    public event EventHandler OnVerticalDownPressedEvent;
    public event EventHandler OnVerticalDownUnpressedEvent;
    private int verticalDirection;
    private bool verticalBusy;
    ///////////////////////////////////////////////////////////////////////

    // Jump Events ////////////////////////////////////////////////////////
    public event EventHandler OnJumpPressedEvent;
    public event EventHandler OnJumpUnpressedEvent;
    ///////////////////////////////////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        // Cancel events //////////////////////////////////////////////////////
        if (Input.GetButtonDown("Cancel"))
        {
            OnCancelPressed();
            if (m_DebugLogsEnabled)
            {
                Debug.Log("Cancel button pressed!");
            }
        }
        if (Input.GetButtonUp("Cancel"))
        {
            OnCancelUnpressed();
            if (m_DebugLogsEnabled)
            {
                Debug.Log("Cancel button unpressed!");
            }
        }
        ///////////////////////////////////////////////////////////////////////

        // Horizontal events //////////////////////////////////////////////////
        if (Input.GetButton("Horizontal"))
        {
            if (!horizontalBusy)
            {
                horizontalBusy = true;
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    OnHorizontalLeftPressed();
                    horizontalDirection = -1;
                    if (m_DebugLogsEnabled)
                    {
                        Debug.Log("Left button pressed!");
                    }
                }
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    OnHorizontalRightPressed();
                    horizontalDirection = 1;
                    if (m_DebugLogsEnabled)
                    {
                        Debug.Log("Right button pressed!");
                    }
                }
            }
        }
        if (Input.GetButtonUp("Horizontal"))
        {
            if ((Input.GetAxisRaw("Horizontal") >= 0 && horizontalDirection < 0))
            {
                OnHorizontalLeftUnpressed();
                horizontalBusy = false;
                if (m_DebugLogsEnabled)
                {
                    Debug.Log("Left button unpressed!");
                }
            }
            else if (Input.GetAxisRaw("Horizontal") <= 0 && horizontalDirection > 0)
            {
                OnHorizontalRightUnpressed();
                horizontalBusy = false;
                if (m_DebugLogsEnabled)
                {
                    Debug.Log("Right button unpressed!");
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////

        // Vertical Events ////////////////////////////////////////////////////
        if (Input.GetButton("Vertical"))
        {
            if (!verticalBusy)
            {
                verticalBusy = true;
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    OnVerticalDownPressed();
                    verticalDirection = -1;
                    if (m_DebugLogsEnabled)
                    {
                        Debug.Log("Down button pressed!");
                    }
                }
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    OnVerticalUpPressed();
                    verticalDirection = 1;
                    if (m_DebugLogsEnabled)
                    {
                        Debug.Log("Up button pressed!");
                    }
                }
            }
        }
        if (Input.GetButtonUp("Vertical"))
        {

            if ((Input.GetAxisRaw("Vertical") >= 0 && verticalDirection < 0))
            {
                OnVerticalDownUnpressed();
                verticalBusy = false;
                if (m_DebugLogsEnabled)
                {
                    Debug.Log("Down button unpressed!");
                }
            }
            else if (Input.GetAxisRaw("Vertical") <= 0 && verticalDirection > 0)
            {
                OnVerticalUpUnpressed();
                verticalBusy = false;
                if (m_DebugLogsEnabled)
                {
                    Debug.Log("Up button unpressed!");
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////

        // Jump Events ////////////////////////////////////////////////////////
        if (Input.GetButtonDown("Jump"))
        {
            OnJumpPressed();
            if (m_DebugLogsEnabled)
            {
                Debug.Log("Jump button pressed!");
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            OnJumpUnpressed();
            if (m_DebugLogsEnabled)
            {
                Debug.Log("Jump button unpressed!");
            }
        }
        ///////////////////////////////////////////////////////////////////////
    }

    // Cancel events //////////////////////////////////////////////////////
    private void OnCancelPressed()
    {
        OnCancelPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnCancelUnpressed()
    {
        OnCancelUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    ///////////////////////////////////////////////////////////////////////

    // Vertical Events ////////////////////////////////////////////////////
    private void OnVerticalUpPressed()
    {
        OnVerticalUpPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnVerticalUpUnpressed()
    {
        OnVerticalUpUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnVerticalDownPressed()
    {
        OnVerticalDownPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnVerticalDownUnpressed()
    {
        OnVerticalDownUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    ///////////////////////////////////////////////////////////////////////

    // Horizontal events //////////////////////////////////////////////////
    private void OnHorizontalLeftPressed()
    {
        OnHorizontalLeftPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnHorizontalLeftUnpressed()
    {
        OnHorizontalLeftUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnHorizontalRightPressed()
    {
        OnHorizontalRightPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnHorizontalRightUnpressed()
    {
        OnHorizontalRightUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    ///////////////////////////////////////////////////////////////////////

    // Jump Events ////////////////////////////////////////////////////////
    private void OnJumpPressed()
    {
        OnJumpPressedEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnJumpUnpressed()
    {
        OnJumpUnpressedEvent?.Invoke(this, EventArgs.Empty);
    }
    ///////////////////////////////////////////////////////////////////////
}
