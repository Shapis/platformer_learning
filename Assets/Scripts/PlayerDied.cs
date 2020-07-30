using System;
using UnityEngine;

public class PlayerDied : MonoBehaviour, IBloodySpikesEvents, IWaterEvents
{
    [SerializeField] private Animator m_PlayerAnimator;
    [SerializeField] private PlayerMovement m_PlayerMovement;
    [SerializeField] private BloodySpikesGrabber m_BloodySpikesGrabber;
    [SerializeField] private WaterGrabber m_WaterGrabber;


    private void Start()
    {
        m_BloodySpikesGrabber.OnBloodySpikesCollisionEnter2DEvent += OnBloodySpikesCollisionEnter2D;
        m_WaterGrabber.OnWaterTriggerEnter2DEvent += OnWaterTriggerEnter2D;
    }
    public void Died()
    {
        m_PlayerAnimator.SetBool("isAirbourne", false);
        m_PlayerAnimator.SetBool("isDead", true);
        m_PlayerMovement.enabled = false;
        StartCoroutine(DelayHandler.DelayAction(2f, () => SceneHandler.ReloadCurrentScene()));
    }
    public void Drowned()
    {
        Died();
    }

    public void OnBloodySpikesCollisionEnter2D(object sender, EventArgs e)
    {
        Died();
    }

    public void OnWaterTriggerEnter2D(object sender, EventArgs e)
    {
        Drowned();
    }

    public void OnWaterTriggerExit2D(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
