using System;
using System.Collections;
using UnityEngine;

public class DustTrail : MonoBehaviour, ICharacterEvents
{
    [SerializeField] private CharacterController2D m_CharacterController2D;
    [SerializeField] private ParticleSystem m_DustTrail;
    private bool isAirbourne = false;
    Coroutine landingDustCoroutine;

    void Start()
    {
        m_CharacterController2D.OnJumpEvent += OnJump;
        m_CharacterController2D.OnHorizontalMovementChangesEvent += OnHorizontalMovementChanges;
        m_CharacterController2D.OnLandingEvent += OnLanding;
        m_CharacterController2D.OnAirbourneEvent += OnAirbourne;
    }


    private void CreateDust()
    {
        m_DustTrail.Play();
    }

    public void OnAirbourne(object sender, EventArgs e)
    {
        isAirbourne = true;
    }

    public void OnFalling(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnHorizontalMovementChanges(object sender, int movementDirection)
    {
        if (landingDustCoroutine != null)
        {
            StopCoroutine(landingDustCoroutine);
        }
        if (!isAirbourne && movementDirection != 0)
        {
            CreateDust();
        }
    }

    public void OnJump(object sender, EventArgs e)
    {
        if (landingDustCoroutine != null)
        {
            StopCoroutine(landingDustCoroutine);
        }
        CreateDust();
    }

    public void OnLanding(object sender, EventArgs e)
    {
        isAirbourne = false;

        if (landingDustCoroutine != null)
        {
            StopCoroutine(landingDustCoroutine);
        }
        landingDustCoroutine = StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        CreateDust();
    }

}
