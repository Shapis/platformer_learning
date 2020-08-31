using System.Collections;
using UnityEngine;

public class PopupMenuController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_TransitionTime = 0.3f;
    [SerializeField] private bool m_StartMinimized = true;
    private Vector3 myInitialScale;
    private float timer;
    private Coroutine myScalingCoroutine;

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
}
