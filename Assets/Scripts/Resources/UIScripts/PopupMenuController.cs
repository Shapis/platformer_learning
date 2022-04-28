using System;
using System.Collections;
using UnityEngine;

public class PopupMenuController : MonoBehaviour, IMenuEvents
{
    [Header("Settings")]
    [SerializeField] private float m_TransitionTime = 0.3f;
    [SerializeField] private bool m_StartMinimized = true;
    private Vector3 myInitialScale;
    private float timer;
    private Coroutine myScalingCoroutine;
    public event EventHandler OnMenuOpenEvent;
    public event EventHandler OnMenuCloseEvent;

    private void Start()
    {
        myInitialScale = gameObject.transform.localScale;
        if (m_StartMinimized)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        CancelScalingCoroutine();
        gameObject.SetActive(true);
        myScalingCoroutine = StartCoroutine("ScaleUp");
        OnMenuOpen(this, EventArgs.Empty);
    }
    public void OnMenuOpen(object sender, EventArgs e)
    {
        OnMenuOpenEvent?.Invoke(sender, EventArgs.Empty);
    }

    private IEnumerator ScaleUp()
    {
        Vector3 localScaleBeforeScalingUp = gameObject.transform.localScale;
        // if (timer < 0f)
        // {
        timer = 0f;
        // }
        while (gameObject.transform.localScale != myInitialScale)
        {
            gameObject.transform.localScale = Vector3.Lerp(localScaleBeforeScalingUp, myInitialScale, timer / m_TransitionTime);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void CloseMenu()
    {
        CancelScalingCoroutine();
        myScalingCoroutine = StartCoroutine("ScaleDown");
        OnMenuClose(this, EventArgs.Empty);
    }

    public void OnMenuClose(object sender, EventArgs e)
    {
        OnMenuCloseEvent?.Invoke(sender, EventArgs.Empty);
    }

    private IEnumerator ScaleDown()
    {
        Vector3 localScaleBeforeScalingDown = gameObject.transform.localScale;
        // if (timer > 1f)
        // {
        timer = 1f;
        // }
        while (gameObject.transform.localScale != Vector3.zero)
        {
            gameObject.transform.localScale = Vector3.Lerp(localScaleBeforeScalingDown, Vector3.zero, (1f - timer) / m_TransitionTime);
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void CancelScalingCoroutine()
    {
        if (myScalingCoroutine != null)
        {
            StopCoroutine(myScalingCoroutine);
        }
    }


    public void OnMenuButtonClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnMenuHover(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
