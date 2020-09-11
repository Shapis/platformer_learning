using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour, ICharacterEvents, IBloodySpikesEvents, IWaterEvents, IDraggableEvents
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private CharacterController2D m_CharacterController2D;
    [SerializeField] private BloodySpikesGrabber m_BloodySpikesGrabber;
    [SerializeField] private WaterGrabber m_WaterGrabber;
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger;
    private GameObject gameObjectCurrentlyBeingDragged = null;
    private bool isFacingRight = true;

    private void Start()
    {
        m_CharacterController2D.OnAirbourneEvent += OnAirbourne;
        m_CharacterController2D.OnLandingEvent += OnLanding;
        m_CharacterController2D.OnFallingEvent += OnFalling;
        //m_CharacterController2D.OnJumpEvent += OnJump;
        m_CharacterController2D.OnHorizontalMovementChangesEvent += OnHorizontalMovementChanges;

        m_BloodySpikesGrabber.OnBloodySpikesCollisionEnter2DEvent += OnBloodySpikesCollisionEnter2D;
        m_WaterGrabber.OnWaterTriggerEnter2DEvent += OnWaterTriggerEnter2D;
        m_PlayerItemDragger.OnDraggingBeginsEvent += OnDraggingBegins;
        m_PlayerItemDragger.OnDraggingEndsEvent += OnDraggingEnds;
    }

    private void Update()
    {
        if (m_Animator.GetBool("isCasting"))
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

    public void OnBloodySpikesCollisionEnter2D(object sender, EventArgs e)
    {
        m_Animator.SetBool("isDead", true);
    }

    public void OnWaterTriggerEnter2D(object sender, EventArgs e)
    {
        m_Animator.SetBool("isDead", true);
    }

    public void OnWaterTriggerExit2D(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDraggingBegins(object sender, PlayerItemDragger.DraggingEventArgs draggingEventArgs)
    {
        gameObjectCurrentlyBeingDragged = draggingEventArgs.TargetGameObject;
        m_Animator.SetBool("isCasting", true);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
        gameObjectCurrentlyBeingDragged = null;
        m_Animator.SetBool("isCasting", false);
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
