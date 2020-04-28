using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelectionController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private List<Button> myButtonList = new List<Button>();
    private int selectedButtonIndex;
    private int maxButtonIndex;

    private bool keyDown;

    [SerializeField] private Color m_UnselectedColor = Color.white;

    [SerializeField] private Color m_SelectedColor = Color.yellow;

    [SerializeField] private float enlargeButtonTo = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Button[] myButtonVector = gameObject.transform.GetComponentsInChildren<Button>();

        foreach (Button b in myButtonVector)
        {
            myButtonList.Add(b);
        }

        maxButtonIndex = gameObject.transform.childCount - 1;

        myButtonList[selectedButtonIndex].gameObject.GetComponent<RectTransform>().localScale = new Vector3(enlargeButtonTo, enlargeButtonTo, enlargeButtonTo);

    }

    // Update is called once per frame
    void Update()
    {

        // Detects input to change the selection up and down.
        InputDetectionUpdater();

        // If nothing gets selected as in, you click somewhere not in a button and it gets de-selected, automatically re-select the previously selected button. 
        // This makes sure there's a button always selected.
        SelectCurrentIndex();

        EnlargeAndChangeChangeColorOfSelectedButton();

        PlaySoundWhenSelected();

    }

    private void PlaySoundWhenSelected()
    {
    }

    private void EnlargeAndChangeChangeColorOfSelectedButton()
    {
        for (int i = 0; i < maxButtonIndex + 1; i++)
        {

            if (i != selectedButtonIndex)
            {
                myButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = m_UnselectedColor;

                if ((myButtonList[i].gameObject.GetComponent<RectTransform>().localScale != new Vector3(1, 1, 1)))
                {

                    Vector3 myTempVector = new Vector3(0.01f, 0.01f, 0.01f);
                    myButtonList[i].gameObject.GetComponent<RectTransform>().localScale -= myTempVector;

                }

            }
            else if (i == selectedButtonIndex)
            {
                myButtonList[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().color = m_SelectedColor;

                if (myButtonList[i].gameObject.GetComponent<RectTransform>().localScale != new Vector3(enlargeButtonTo, enlargeButtonTo, enlargeButtonTo))
                {
                    Vector3 myTempVector = new Vector3(0.01f, 0.01f, 0.01f);
                    myButtonList[i].gameObject.GetComponent<RectTransform>().localScale += myTempVector;
                }

            }

        }
    }

    private void InputDetectionUpdater()
    {
        if (Input.GetAxis("Vertical") != 0)
        {

            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (selectedButtonIndex < maxButtonIndex)
                    {
                        selectedButtonIndex++;
                    }
                    else
                    {
                        selectedButtonIndex = 0;
                    }

                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (selectedButtonIndex > 0)
                    {
                        selectedButtonIndex--;
                    }
                    else
                    {
                        selectedButtonIndex = maxButtonIndex;
                    }

                }

            }

            keyDown = true;
            //            Debug.Log(selectedButtonIndex + " " + keyDown);
        }
        else
        {
            keyDown = false;
        }
    }

    private void SelectCurrentIndex()
    {
        // If nothing gets selected as in, you click somewhere not in a button and it gets de-selected, automatically re-select the previously selected button. This makes sure there's a button always selected.
        // if (EventSystem.current.currentSelectedGameObject == null)
        // {
        myButtonList[selectedButtonIndex].Select();

        // }
    }

    public void WhenSelected(Button b)
    {
        b.Select();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        for (int i = 0; i < myButtonList.Count; i++)
        {

            if (myButtonList[i] == eventData.pointerEnter.GetComponent<Button>())
            {
                selectedButtonIndex = i;
                // Debug.Log(i);
            }

        }

        WhenSelected(myButtonList[selectedButtonIndex]);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pointer clicked");
    }
}