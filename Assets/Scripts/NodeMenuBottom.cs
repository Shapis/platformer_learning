using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class NodeMenuBottom : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Image m_BackgroundCenter;
    [SerializeField] private Image m_BackgroundOutline;
    [SerializeField] private TextMeshProUGUI m_PlayText;

    [Header("Settings")]
    [SerializeField] private Color[] m_PlayTextColors;
    [SerializeField] private Color[] m_PlayBackgroundCenterColors;
    [SerializeField] private Color[] m_PlayBackgroundOutlineColors;
    private int myCurrentColor = 0;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeColors", 0f, 1f);
        //StartCoroutine("VaryColor");
    }


    private IEnumerator VaryColor()
    {
        float timer = 0f;
        Color temp = m_BackgroundCenter.color;
        while (m_BackgroundCenter.color != m_PlayBackgroundCenterColors[1])
        {
            m_BackgroundCenter.color = Color.Lerp(temp, m_PlayBackgroundCenterColors[1], timer / 10f);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        Debug.Log(timer);
    }

    private void ChangeColors()
    {
        if (myCurrentColor >= m_PlayTextColors.Length)
        {
            myCurrentColor = 0;
        }
        m_BackgroundCenter.color = m_PlayBackgroundCenterColors[myCurrentColor];
        m_PlayText.color = m_PlayTextColors[myCurrentColor];
        m_BackgroundOutline.color = m_PlayBackgroundOutlineColors[myCurrentColor];
        myCurrentColor++;
    }
}
