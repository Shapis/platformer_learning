using System.Collections;
using UnityEngine;

public class PopupMenuController : MonoBehaviour
{

    [SerializeField] private float transitionTime = 0.3f;

    GameObject closeButton;
    GameObject mainPanel;

    [SerializeField] Vector3 initialScale = new Vector3(0, 0, 0);

    public bool IsActive { get; set; }


    private void Awake()
    {
        closeButton = this.gameObject.transform.GetChild(0).gameObject;  // set exitButton to the background exit button.
        mainPanel = this.gameObject.transform.GetChild(1).gameObject;   // set mainPanel to the main pop up panel.
        mainPanel.GetComponent<RectTransform>().localScale = initialScale;      // Awakes the Main Panel scaled to 0.
    }

    private void Start()
    {


    }



    public void OpenMenu()
    {
        // if (LeanTween.isTweening(mainPanel))
        // {
        //     LeanTween.cancel(mainPanel);
        // }

        this.gameObject.SetActive(true);
        closeButton.SetActive(true);
        LeanTween.scale(mainPanel, new Vector3(1f, 1f, 1f), transitionTime).setUseEstimatedTime(true);

        IsActive = true;
    }

    public void CloseMenu()
    {
        if (LeanTween.isTweening(mainPanel))
        {
            LeanTween.cancel(mainPanel);
        }
        closeButton.SetActive(false);
        LeanTween.scale(mainPanel, new Vector3(0f, 0f, 0f), transitionTime).setUseEstimatedTime(true);
        IsActive = false;

        LeanTween.delayedCall(transitionTime, SetObjectInactiveAfterTweenIsOver).setUseEstimatedTime(true);
    }

    private void SetObjectInactiveAfterTweenIsOver()
    {
        if (!IsActive && !LeanTween.isTweening(mainPanel))
        {
            this.gameObject.SetActive(false);

        }
    }
}
