using System.Collections;
using UnityEngine;

public class NodeInnerCircleColor : MonoBehaviour
{

    [SerializeField] private Color[] m_NotAccessibleColors;
    [SerializeField] private Color[] m_AccessibleColors;
    private SpriteRenderer mySpriteRenderer;

    private int myCurrentColor = 0;



    private void Start()
    {
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine("ChangeColorsEverySecond");
    }

    private IEnumerator ChangeColorsEverySecond()
    {
        // This is necessary to avoid some initialization issues, since this Coroutine is called on Start(), and some calculations
        // are still happening to figure out which nodes are accessible and which aren't.
        yield return new WaitForEndOfFrame();

        while (true)
        {
            ChangeColors();
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    void ChangeColors()
    {
        if (!gameObject.transform.parent.gameObject.GetComponent<Node>().IsAccessible)
        {
            if (myCurrentColor >= m_NotAccessibleColors.Length)
            {
                myCurrentColor = 0;
            }
            mySpriteRenderer.color = m_NotAccessibleColors[myCurrentColor];
            myCurrentColor++;
        }
        else
        {
            if (myCurrentColor >= m_AccessibleColors.Length)
            {
                myCurrentColor = 0;
            }
            mySpriteRenderer.color = m_AccessibleColors[myCurrentColor];
            myCurrentColor++;

        }
    }



}
