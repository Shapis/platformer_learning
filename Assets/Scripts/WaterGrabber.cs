using System;
using System.Collections;
using UnityEngine;

public class WaterGrabber : MonoBehaviour, IWaterEvents
{
    private float m_MaxVelocity = 1f;
    private bool isUnderwater = false;
    private Rigidbody2D rb;
    private float upwardsVelocity = 0f;
    private Coroutine waterTriggerExit2DCoroutine = null;
    private Coroutine waterTriggerEnter2DCoroutine = null;
    public event EventHandler OnWaterTriggerEnter2DEvent;
    public event EventHandler OnWaterTriggerExit2DEvent;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Water>() != null && !isUnderwater)
        {
            if (waterTriggerExit2DCoroutine != null)
            {
                StopCoroutine(waterTriggerExit2DCoroutine);
            }
            isUnderwater = true;
            waterTriggerEnter2DCoroutine = StartCoroutine("WaterTriggerEnter2D");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Water>() != null && isUnderwater)
        {
            if (waterTriggerEnter2DCoroutine != null)
            {
                StopCoroutine(waterTriggerEnter2DCoroutine);
            }
            isUnderwater = false;
            waterTriggerExit2DCoroutine = StartCoroutine("WaterTriggerExit2D");
        }
    }

    private IEnumerator WaterTriggerEnter2D()
    {
        yield return new WaitForSeconds(0.2f);
        OnWaterTriggerEnter2D(this, EventArgs.Empty);
    }

    private IEnumerator WaterTriggerExit2D()
    {
        yield return new WaitForSeconds(0.4f);
        OnWaterTriggerExit2D(this, EventArgs.Empty);
    }

    private void FixedUpdate()
    {
        if (isUnderwater)
        {
            if (rb.velocity.y < -m_MaxVelocity * 3)
            {
                rb.velocity = new Vector2(rb.velocity.x, -m_MaxVelocity * 3);
            }
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

    public void OnWaterTriggerEnter2D(object sender, EventArgs e)
    {
        OnWaterTriggerEnter2DEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnWaterTriggerExit2D(object sender, EventArgs e)
    {
        OnWaterTriggerExit2DEvent?.Invoke(this, EventArgs.Empty);
    }
}
