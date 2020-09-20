using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour, ICharacterEvents, IBloodySpikesEvents, IWaterEvents, IDraggableEvents
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private CharacterController2D m_CharacterController2D;
    [SerializeField] private BloodySpikesGrabber m_BloodySpikesGrabber;
    [SerializeField] private WaterGrabber m_WaterGrabber;
    [SerializeField] private PlayerItemDragger m_PlayerItemDragger;
    private GameObject gameObjectCurrentlyBeingDragged = null;
    private Coroutine squeezeCoroutine;

    private void Start()
    {
        m_CharacterController2D.OnAirbourneEvent += OnAirbourne;
        m_CharacterController2D.OnLandingEvent += OnLanding;
        m_CharacterController2D.OnFallingEvent += OnFalling;
        m_CharacterController2D.OnJumpEvent += OnJump;
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
                    //Flip(-1); // If the player is to the left of the object being dragged, make the player face left if he isn't currently facing left.
                }
                else
                {
                    //Flip(1); // If the player is to the right of the object being dragged, make the player face right if he isn't currently facing righht.
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
        if (squeezeCoroutine != null)
        {
            StopCoroutine(squeezeCoroutine);
        }
        squeezeCoroutine = StartCoroutine(JumpSqueeze(1.25f, 0.6f, 0.07f));
        m_Animator.SetBool("isAirbourne", false);
    }

    public void OnFalling(object sender, EventArgs e)
    {

        m_Animator.SetBool("isAirbourne", true);
    }

    public void OnJump(object sender, EventArgs e)
    {
        if (squeezeCoroutine != null)
        {
            StopCoroutine(squeezeCoroutine);
        }
        squeezeCoroutine = StartCoroutine(JumpSqueeze(0.7f, 1.2f, 0.1f));
    }

    public void OnHorizontalMovementChanges(object sender, int movementDirection)
    {
        bool isMoving = false;

        if (movementDirection != 0)
        {
            isMoving = true;
        }
        m_Animator.SetBool("isMoving", isMoving);

        //Flip(movementDirection);
    }

    private IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = transform.localScale;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(originalSize, newSize, timer);
            transform.localPosition = new Vector2(transform.localPosition.x, -(1 - transform.localScale.y) / 2);
            yield return null;
        }
        timer = 0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / seconds;
            transform.localScale = Vector3.Lerp(newSize, Vector3.one, timer);
            transform.localPosition = new Vector2(transform.localPosition.x, -(1 - transform.localScale.y) / 2);
            yield return null;
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
        m_Animator.SetBool("isCasting", true);
    }

    public void OnDraggingEnds(object sender, EventArgs e)
    {
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
