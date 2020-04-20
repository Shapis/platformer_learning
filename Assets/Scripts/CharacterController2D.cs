using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour, ICharacterEvents
{

    [SerializeField] private float m_JumpIntensity = 400f; // Amount of force/velocity added when the unit jumps.
    [SerializeField] private bool m_UseVelocityForJumping = false; // Change the unit's y axis velocity instead of adding y force when jumping.

    [SerializeField] private bool m_AirControl = false; // Whether or not a player can steer while jumping;
    [SerializeField] [Range(0.0f, 5f)] private float m_CoyoteTime = 0.2f; // Duration of coyote time.
    [SerializeField] private bool m_DoubleJump = false; // Whether the player can double jump.
    [SerializeField] private int m_NumberOfDoubleJumps = 1; // If Double jumps are enabled, this is the amount of times a player is allowed to double jump by default.

    [Range(0, 1f)] [SerializeField] private float m_CrouchSpeed = .36f; // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // How much to smooth out the movement

    [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck; // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck; // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider; // A collider that will be disabled when crouching

    private bool collisionJump = false;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private float myCoyoteStartTime = 0f;
    private bool myCoyoteJump = false;

    private int m_DoubleJumpsRemaining;

    private float previousFeetPosition;
    //private bool m_CurrentDoubleJump = false;
    private bool isFalling = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandingEvent; // When a unit initially lands.

    public UnityEvent OnAirbourneEvent; // When the unit first becomes airbourne.

    public UnityEvent OnFallingEvent; // When a unit starts falling. As in, your first airbourne position.y is lower than your previous position.y

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchingEvent;
    private bool m_wasCrouching = false;

    private bool freeFallDoubleJumpSwitch = true;

    public int DoubleJumpsRemaining { get => m_DoubleJumpsRemaining; set => m_DoubleJumpsRemaining = value; }

    private bool grounded = false;

    public int NumberOfDoubleJumps { get => m_NumberOfDoubleJumps; set => m_NumberOfDoubleJumps = value; }
    public bool IsFalling { get => isFalling; set => isFalling = value; }
    public bool Grounded { get => grounded; set => grounded = value; }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        previousFeetPosition = m_GroundCheck.transform.position.y;
        DoubleJumpsRemaining = m_NumberOfDoubleJumps;
        m_DoubleJumpsRemaining = 0;

        if (!m_DoubleJump)
        {
            m_NumberOfDoubleJumps = 0;
        }

        if (OnFallingEvent == null)
        {
            OnFallingEvent = new UnityEvent();
        }


    }
    private void Start()
    {

        OnLandingEvent?.AddListener(OnLandingListener);
        OnFallingEvent?.AddListener(OnFallingListener);
        OnAirbourneEvent?.AddListener(OnAirbourneListener);
        //OnCrouchingEvent?.AddListener(OnCrouchingListener);

    }

    private void FixedUpdate()
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////////// Events



        bool previouslyGrounded = grounded; // previouslyGrounded keeps the information of whether the unit was grounded the previous frame.

        grounded = GroundedCheck(); // Checks whether the unit is currently grounded and assigns it to the grounded field.


        if (LandingCheck(previouslyGrounded))
            OnLanding(); // If the unit has landed invokes the OnLandingEvent.

        if (AirbourneCheck(previouslyGrounded))
            OnAirbourne(); // If the unit is airbourne invokes the OnAirbourneEvent.

        if (FallingCheck(AirbourneCheck(previouslyGrounded), previousFeetPosition))
            OnFalling(); // If the unit is airbourne, falling and wasnt explicitly because of a jump run invokes the OnFallingEvent.

        // Updates the previous feet position to the current feet ground position so you can check if the unit is falling while airbourne the next frame.
        previousFeetPosition = m_GroundCheck.transform.position.y;

        if (CrouchingCheck())
            OnCrouching(); // If the Unit is Crouching, Invokes the OnCrouchingEvent.

        ///////////////////////////////////////////////////////////////////////////////////////////////////////// //Events//



        CoyoteTime();

    }

    private void CoyoteTime()
    {
        if (myCoyoteJump && Time.time >= m_CoyoteTime + myCoyoteStartTime)
        {
            myCoyoteJump = false;
            //Debug.Log("Coyote jump is off now");
        }

        if (!myCoyoteJump && isFalling)
        {
            //Debug.Log("freefall");
            m_DoubleJumpsRemaining = 0;
            //isFalling = false;

        }

    }

    private void OnLandingListener()
    {
        DoubleJumpsRemaining = m_NumberOfDoubleJumps;
        isFalling = false;

    }

    private void OnAirbourneListener()
    {

    }


    private void OnFallingListener()
    {
        isFalling = true;
        myCoyoteStartTime = Time.time;
        myCoyoteJump = true;
        //Debug.Log(gameObject + "OnFallingListener called.");
        //Debug.Log(myCoyoteJump && Time.time >= m_CoyoteTime + myCoyoteStartTime);


        m_DoubleJumpsRemaining++;

    }

    private void OnCrouchingListener()
    {

    }

    private bool GroundedCheck()
    {

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as to be considered as ground, checking for which layers are ground m_WhatIsGround
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        if (colliders.Length > 0)
        {

            return true;

        }
        else
        {
            return false;
        }

    }

    private bool LandingCheck(bool previouslyGrounded)
    {
        return (grounded && !previouslyGrounded);
    }

    public void OnLanding()
    {


        OnLandingEvent?.Invoke();
        //Debug.Log(gameObject.name + " landed at: " + gameObject.transform.position + " Time: " + Time.time);


    }

    private bool AirbourneCheck(bool previouslyGrounded)
    {
        return (!grounded && previouslyGrounded);
    }

    public void OnAirbourne()
    {

        OnAirbourneEvent?.Invoke();
        //Debug.Log(gameObject.name + " airbourne at: " + gameObject.transform.position + " Time: " + Time.time);

        // If double jumps are enabled max out the number of max double jumps left when unit becomes airbourne


        // Checks if the unit is falling and implementation of coyote time.


    }

    private bool FallingCheck(bool isAirbourne, float previousFeetPosition)
    {

        return (isAirbourne && (m_GroundCheck.transform.position.y < previousFeetPosition));
    }

    public void OnFalling()
    {


        OnFallingEvent?.Invoke();
        //Debug.Log(gameObject.name + " falling at: " + gameObject.transform.position + " Time: " + Time.time);




    }

    private bool CrouchingCheck()
    {
        return false;
    }

    public void OnCrouching()
    {

    }



    public void Move(float move, bool crouch, bool jump, bool collisionJump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchingEvent?.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchingEvent?.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...

        if ((grounded && jump) || (myCoyoteJump && jump) || ((DoubleJumpsRemaining > 0) && jump && m_DoubleJump))

        {

            // Only reduces charges of double jumps if you're currently not ground jumping and not coyote jumping
            if (!grounded && !myCoyoteJump)
            {
                DoubleJumpsRemaining--;
            }

            // If unit is falling and outside of coyote time then don't jump.
            if (isFalling && !myCoyoteJump)
            {
                return;
            }

            if (isFalling && myCoyoteJump)
            {
                m_DoubleJumpsRemaining--;
            }

            // Add vertical force/velocity to the unit.
            Jump();



        }
        else if (collisionJump)

        {

            if (m_DoubleJump)
            {

                //while (DoubleJumpsRemaining < m_NumberOfDoubleJumps)
                //{
                DoubleJumpsRemaining++;
                //}

            }


            // Add vertical force/velocity to the unit.
            Jump();

            collisionJump = false;
        }

        // Add vertical force/velocity to the unit.
        void Jump()
        {
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

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }


    public void Casting()
    {
        m_Rigidbody2D.velocity = new Vector3(0, 0, 0);

        if (gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.transform.position.x > GameObject.Find("Player").transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (gameObject.GetComponent<PlayerMagic>().ObjectBeingDragged.transform.position.x < GameObject.Find("Player").transform.position.x && m_FacingRight)
        {
            Flip();
        }

    }


}