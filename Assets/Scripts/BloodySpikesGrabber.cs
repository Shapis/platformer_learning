using System;
using UnityEngine;

public class BloodySpikesGrabber : MonoBehaviour, IBloodySpikesEvents
{
    public event EventHandler OnBloodySpikesCollisionEnter2DEvent;
    private void OnCollisionEnter2D(Collision2D other)
    {
        BloodySpikes bloodySpikes = other.gameObject.GetComponent<BloodySpikes>();
        if (bloodySpikes != null && bloodySpikes.Tangible)
        {
            bloodySpikes.Tangible = false;
            OnBloodySpikesCollisionEnter2D(this, EventArgs.Empty);
        }
    }
    public void OnBloodySpikesCollisionEnter2D(object sender, EventArgs e)
    {
        OnBloodySpikesCollisionEnter2DEvent?.Invoke(this, EventArgs.Empty);
    }
}
