using System;
using System.Collections;
using UnityEngine;

public class CharacterController2D : MonoBehaviour, ICharacterEvents, IDraggableEvents
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character A position marking where to check for ceilings
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger; // Dependency so we know when to flip the character to face the direction of the item that is being dragged.

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private bool m_AirControl = true; // Whether or not a player can steer while jumping;
    [SerializeField] private float m_JumpIntensity = 400f; // Amount of force/velocity added when the unit jumps.
    [SerializeField] private bool m_UseVelocityForJumping = true; // Change the unit's y axis velocity instead of adding y force when jumping.
    [SerializeField] [Range(0.0f, 5f)] private float m_CoyoteTime = 0.2f; // Duration of coyote time.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement

    private const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Vector3 m_Velocity = Vector3.zero;
    private float myCoyoteStartTime;
    private bool myCoyoteJump = false;
    private bool isFalling;
    public bool isGrounded;
    private bool jumpKeyPressed = false;
    private bool previouslyJumpKeyPressed = false;
    private Vector3 previousPosition;
    private int previousMovementDirection;
    public event EventHandler OnAirbourneEvent; // When the unit first becomes airbourne.
    public event EventHandler OnLandingEvent; // When a unit initially lands.
    public event EventHandler OnFallingEvent; // When a unit starts falling. As in, your first airbourne position.y is lower than your previous position.y or your first airbourne position after reaching peak is lower than the previous one.
    public event EventHandler<int> OnHorizontalMovementChangesEvent;
    public event EventHandler OnJumpEvent;
    private bool isFacingRight = true;
    GameObject gameObjectCurrentlyBeingDragged = null;


    private void Start()
    {
        previousPosition = transform.position;

        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
    }

    private void FixedUpdate()
    {
        bool previouslyGrounded = isGrounded; // previouslyGrounded keeps the information of whether the unit was grounded the previous frame.

        isGrounded = GroundedCheck(); // Checks whether the unit is currently grounded and assigns it to the grounded property.

        if (LandingCheck(previouslyGrounded))
            OnLanding(this, EventArgs.Empty); // If the unit has landed invokes the OnLandingEvent.

        if (AirbourneCheck(previouslyGrounded))
            OnAirbourne(this, EventArgs.Empty); // If the unit is airbourne invokes the OnAirbourneEvent.

        if (FallingCheck(AirbourneCheck(previouslyGrounded), previousPosition.y))
            OnFalling(this, EventArgs.Empty); // If the unit is airbourne, falling and wasnt explicitly because of a jump run invokes the OnFallingEvent.

        // Updates the previous feet position to the current feet ground position so you can check if the unit is falling while airbourne the next frame.
        previousPosition = transform.position;

        CoyoteTime();
        FaceTheObjectBeingDragged(); // ? I'm not sure if this should be in FixedUpdate() or in Update() but it seems to be workign fine in here ?

    }

    private void FaceTheObjectBeingDragged()
    {
        if (gameObjectCurrentlyBeingDragged != null)
        {
            if (gameObject.transform.position.x - gameObjectCurrentlyBeingDragged.transform.position.x > 0)
            {
                Flip(-1); // If the player is to the left of the object being dragged, make the player face left if he isn't currently facing left.
            }
            else
            {
                Flip(1); // If the player is to the right of the object being dragged, make the player face right if he isn't currently facing righht.
            }
        }
    }

    private void CoyoteTime()
    {
        if (myCoyoteJump && Time.time >= m_CoyoteTime + myCoyoteStartTime)
        {
            myCoyoteJump = false;
        }
    }

    private bool GroundedCheck()
    {
        float rayCastLength = 0.54f;
        Debug.DrawRay(transform.position + Vector3.left * 0.25f, Vector3.down * rayCastLength, Color.red);
        Debug.DrawRay(transform.position + Vector3.right * 0.15f, Vector3.down * rayCastLength, Color.red);
        //RaycastHit2D abc = Physics2D.Raycast(transform.position + Vector3.left * 0.25f, Vector2.down, rayCastLength, m_WhatIsGround);
        return Physics2D.Raycast(transform.position + Vector3.left * 0.25f, Vector2.down, rayCastLength, m_WhatIsGround) || Physics2D.Raycast(transform.position + Vector3.right * 0.15f, Vector2.down, rayCastLength, m_WhatIsGround);
    }

    private bool LandingCheck(bool previouslyGrounded)
    {
        return isGrounded && !previouslyGrounded;
    }

    public void OnLanding(object sender, EventArgs e)
    {
        isFalling = false;
        OnLandingEvent?.Invoke(this, EventArgs.Empty);
    }

    private bool AirbourneCheck(bool previouslyGrounded)
    {
        return !isGrounded && previouslyGrounded;
    }

    public void OnAirbourne(object sender, EventArgs e)
    {
        OnAirbourneEvent?.Invoke(this, EventArgs.Empty);
    }

    private bool FallingCheck(bool isAirbourne, float previousPositionY)
    {
        return isAirbourne && (transform.position.y < previousPositionY);
    }

    public void OnFalling(object sender, EventArgs e)
    {
        isFalling = true;
        myCoyoteStartTime = Time.time;
        myCoyoteJump = true;
        OnFallingEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnHorizontalMovementChanges(object sender, int movementDirection)
    {
        OnHorizontalMovementChangesEvent?.Invoke(this, movementDirection);
    }

    public void OnJump(object sender, EventArgs e)
    {
        OnJumpEvent?.Invoke(this, EventArgs.Empty);
    }

    public void Move(int movementDirection, bool jumpKeyPressed)
    {
        //only control the player if grounded or m_AirControl is turned on
        if (isGrounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(movementDirection * movementSpeed, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            MovementDirectionChangesChecker(movementDirection);
        }
        // If the player should jump...
        if ((isGrounded && jumpKeyPressed && !previouslyJumpKeyPressed) || (myCoyoteJump && jumpKeyPressed && !previouslyJumpKeyPressed))
        {
            // If unit is falling and outside of coyote time then don't jump.
            if (isFalling && !myCoyoteJump)
            {
                return;
            }
            // Add vertical force/velocity to the unit.
            Jump();
        }

        if (previouslyJumpKeyPressed && !jumpKeyPressed)
        {
            previouslyJumpKeyPressed = false;
            if (m_Rigidbody2D.velocity.y > 0)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.9f);

                StartCoroutine("SlowDown");
            }
        }
    }

    IEnumerator SlowDown()
    {
        yield return new WaitForFixedUpdate();
        while (m_Rigidbody2D.velocity.y > 0)
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y * 0.95f);
            yield return null;
        }
    }

    private void MovementDirectionChangesChecker(int movementDirection)
    {
        if (movementDirection > 0 & previousMovementDirection <= 0)
        {
            OnHorizontalMovementChanges(this, 1);
        }
        else if (movementDirection < 0 & previousMovementDirection >= 0)
        {
            OnHorizontalMovementChanges(this, -1);
        }
        else if (movementDirection == 0 & previousMovementDirection != 0)
        {
            OnHorizontalMovementChanges(this, 0);
        }

        if (movementDirection > 0)
        {
            previousMovementDirection = 1;
        }
        else if (movementDirection < 0)
        {
            previousMovementDirection = -1;
        }
        else if (movementDirection == 0)
        {
            previousMovementDirection = 0;
        }

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

    // Add vertical force/velocity to the unit.
    private void Jump()
    {
        OnJump(this, EventArgs.Empty);

        this.previouslyJumpKeyPressed = true;

        if (m_UseVelocityForJumping)
        {
            m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, m_JumpIntensity / 50, 0);
        }
        else
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpIntensity));
        }

        // After the unit jumps it is no longer falling.
        isFalling = false;
        myCoyoteJump = false;
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        gameObjectCurrentlyBeingDragged = draggingEventArgs.TargetGameObject;
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        gameObjectCurrentlyBeingDragged = null;
    }

    public void OnLineOfSightBlocked(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        throw new NotImplementedException();
    }

    public void OnLineOfSightUnblocked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}