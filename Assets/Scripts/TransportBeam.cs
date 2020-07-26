using System.Collections;
using UnityEngine;
using System;

public class TransportBeam : BaseItem, IPressurePlateEvents, ITransportBeamEvents
{
    [Header("Settings")]
    [SerializeField] private ColorPalette.ColorName m_BeamColor;
    [SerializeField] private float m_FullAnimationTimeInSeconds = 1f;
    [SerializeField] private bool m_StartMinimized = true;
    [SerializeField] private float m_FloatingStrength = 1f;

    private bool m_DebugLoggingEnabled = false;

    public float GetFloatingStrength()
    {
        return m_FloatingStrength;
    }

    private Vector3 totalLength;
    private Vector3 minimizedLength;
    private Vector3 initialPosition;
    private float animationCompletionPercentage = 1f;

    public event EventHandler OnTransportBeamExpandBeginsEvent;
    public event EventHandler OnTransportBeamExpandEndsEvent;
    public event EventHandler OnTransportBeamCollapseBeginsEvent;
    public event EventHandler OnTransportBeamCollapseEndsEvent;

    private void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red); is a generic way of interacting with the color variable.
        // material.color will only be available if the name of the reference of the color is set to _Color in shadergraph
        gameObject.GetComponent<SpriteRenderer>().material.color = ColorPalette.GetColor32(m_BeamColor);

        // Subscribes to every pressure plate in the scene.
        foreach (var o in (PressurePlate[])GameObject.FindObjectsOfType(typeof(PressurePlate)))
        {
            o.OnPressurePlateActivatedEvent += OnPressurePlateActivated;
            o.OnPressurePlateDeactivatedEvent += OnPressurePlateDeactivated;
        }

        totalLength = gameObject.transform.localScale;

        minimizedLength = new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);

        initialPosition = gameObject.transform.position;

        if (m_StartMinimized)
        {
            gameObject.transform.localScale = minimizedLength;
            animationCompletionPercentage = 0f;
        }
    }

    private void StartTransportBeam()
    {
        StopAllCoroutines();
        StartCoroutine("DoExpand");
    }

    private void EndTransportBeam()
    {
        StopAllCoroutines();
        StartCoroutine("DoCollapse");
    }

    public void OnPressurePlateActivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        if (m_BeamColor == pressurePlateColor)
        {
            StartTransportBeam();
        }
    }

    public void OnPressurePlateDeactivated(object sender, ColorPalette.ColorName pressurePlateColor)
    {
        if (m_BeamColor == pressurePlateColor)
        {
            EndTransportBeam();
        }
    }

    IEnumerator DoExpand()
    {
        OnTransportBeamExpandBegins(this, EventArgs.Empty);
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.localScale != totalLength)
        {
            animationCompletionPercentage += 1 / 60f;
            gameObject.transform.localScale = Vector3.Lerp(minimizedLength, totalLength, animationCompletionPercentage / m_FullAnimationTimeInSeconds);

            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            initialPosition.y + (gameObject.transform.localScale.y * 0.01f / 2f) - (totalLength.y * 0.01f / 2f),
            gameObject.transform.position.z);

            yield return null;
        }
        if (animationCompletionPercentage > 1f)
        {
            animationCompletionPercentage = 1f;
        }
        OnTransportBeamExpandEnds(this, EventArgs.Empty);

    }

    // TODO: This doesn't pause when the game is paused even though WaitForSeconds() is supposed to respect the time scale.
    IEnumerator DoCollapse()
    {
        OnTransportBeamCollapseBegins(this, EventArgs.Empty);
        yield return new WaitForSeconds(1 / 60);
        while (gameObject.transform.localScale != minimizedLength)
        {
            animationCompletionPercentage -= 1 / 60f;
            gameObject.transform.localScale = Vector3.Lerp(minimizedLength, totalLength, animationCompletionPercentage / m_FullAnimationTimeInSeconds);

            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
            initialPosition.y + (gameObject.transform.localScale.y * 0.01f / 2f) - (totalLength.y * 0.01f / 2f),
            gameObject.transform.position.z);

            yield return null;
        }
        if (animationCompletionPercentage < 0f)
        {
            animationCompletionPercentage = 0f;
        }
        OnTransportBeamCollapseEnds(this, EventArgs.Empty);
    }

    public void OnTransportBeamExpandBegins(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamExpandBeginsEvent");
        }
        OnTransportBeamExpandBeginsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnTransportBeamExpandEnds(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamExpandEndsEvent");
        }
        OnTransportBeamExpandEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnTransportBeamCollapseBegins(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamCollapseBeginsEvent");
        }
        OnTransportBeamCollapseBeginsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnTransportBeamCollapseEnds(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamCollapseEndsEvent");
        }
        OnTransportBeamCollapseEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnTransportBeamTriggerEnter2D(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamTriggerEnter2D");
        }
        throw new NotImplementedException();
    }

    public void OnTransportBeamTriggerExit2D(object sender, EventArgs e)
    {
        if (m_DebugLoggingEnabled)
        {
            Debug.Log("OnTransportBeamTriggerExit2D");
        }
        throw new NotImplementedException();
    }
}
