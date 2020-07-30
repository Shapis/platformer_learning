using System;
using UnityEngine;

public class TransportBeamGrabber : MonoBehaviour, ITransportBeamEvents
{
    [SerializeField] private float m_MaxVelocity = 2f;
    private Rigidbody2D rb;
    private TransportBeam transportBeam;
    private TransportBeam[] myTransportBeams;
    private bool insideBeam = false;
    private float upwardsVelocity = 0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        foreach (var o in (TransportBeam[])GameObject.FindObjectsOfType(typeof(TransportBeam)))
        {
            o.OnTransportBeamCollapseEndsEvent += OnTransportBeamCollapseEnds;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TransportBeam>() != null && other.gameObject.GetComponent<TransportBeam>().Tangible)
        {
            transportBeam = other.gameObject.GetComponent<TransportBeam>();
            insideBeam = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TransportBeam>() != null && other.gameObject.GetComponent<TransportBeam>().Tangible)
        {
            ExitTrigger2D();
        }
    }

    private void FixedUpdate()
    {
        if (insideBeam)
        {
            if (rb.velocity.y < m_MaxVelocity)
            {
                upwardsVelocity++;
                rb.velocity += Vector2.up * upwardsVelocity * Time.fixedDeltaTime;
            }
        }
        else if (upwardsVelocity > 0)
        {
            upwardsVelocity -= 0.5f;
        }
    }

    // Keep in mind that it's possible that this happens more than once, since it happens when both the current TransportBeam fully collapses or when
    // the gameObject this TransportBeamGrabber.cs is attached to leaves the trigger area.
    private void ExitTrigger2D()
    {
        insideBeam = false;
        transportBeam = null;
        //Debug.Log(gameObject.transform.name + " left the trigger area");
        //upwardsVelocity = 0f;
    }

    public void OnTransportBeamExpandBegins(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnTransportBeamExpandEnds(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnTransportBeamCollapseBegins(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnTransportBeamCollapseEnds(object sender, EventArgs e)
    {
        TransportBeam senderBeam = (TransportBeam)sender;

        if (senderBeam == transportBeam)
        {
            ExitTrigger2D();
        }
    }

    public void OnTransportBeamTriggerEnter2D(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnTransportBeamTriggerExit2D(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
