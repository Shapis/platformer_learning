using UnityEngine;

public class NodeInnerCircleColor : MonoBehaviour
{

    [SerializeField] private Color[] m_NotAccessibleColors;
    [SerializeField] private Color[] m_AccessibleColors;

    [SerializeField] private float m_RepeatRate;
    private SpriteRenderer mySpriteRenderer;

    private int myCurrentColor = 0;



    private void Start()
    {
        mySpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColors", 0, m_RepeatRate);
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
