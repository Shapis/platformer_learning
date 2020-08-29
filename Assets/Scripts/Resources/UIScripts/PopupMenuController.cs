using System.Collections;
using UnityEngine;

public class PopupMenuController : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float transitionTime = 0.3f;
    [SerializeField] private bool m_StartMinimized = true;

    private int myTweenScaleUp;
    private int myTweenScaleDown;
    private int myTweenDelayedCall;

    private Vector3 myInitialScale;


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
        myInitialScale = gameObject.transform.localScale;
        MinimizedSettings();
        LeanTween.cancel(myTweenScaleDown);
        LeanTween.cancel(myTweenDelayedCall);
        gameObject.SetActive(true);
        myTweenScaleUp = LeanTween.scale(gameObject, myInitialScale, transitionTime).setUseEstimatedTime(true).id;
    }



    public void CloseMenu()
    {
        MinimizedSettings();
        LeanTween.cancel(myTweenScaleUp);
        myTweenScaleDown = LeanTween.scale(gameObject, new Vector3(0f, 0f, 0f), transitionTime).setUseEstimatedTime(true).id;
        //myTweenDelayedCall = LeanTween.delayedCall(transitionTime, SetGameObjectInactive).setUseEstimatedTime(true).id;
    }

    public void StartOpenAndMaximized()
    {
        this.m_StartMinimized = false;
        gameObject.transform.localScale = myInitialScale;
        gameObject.SetActive(true);
    }

    // private void SetGameObjectInactive()
    // {
    //     gameObject.SetActive(false);
    // }

    private void MinimizedSettings()
    {
        if (m_StartMinimized)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            m_StartMinimized = false;
        }
    }
}
