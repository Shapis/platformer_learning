using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHolder : MonoBehaviour, IKeyGrabberEvents
{
    [Header("Dependencies")]

    [SerializeField] private Transform m_KeyTemplate;
    private GameObject m_Player;
    private KeyGrabber m_KeyGrabber;
    private Transform m_Container;

    private void Start()
    {
        m_Container = transform.Find("ContainerKeys");
        m_KeyGrabber = GameObject.Find("Player").GetComponent<KeyGrabber>();
        m_KeyTemplate.gameObject.SetActive(false);
        m_KeyGrabber.OnKeysChangedEvent += OnKeysChanged;
    }

    private void UpdateVisual(List<ColorPalette.ColorName> keyList)
    {
        // Clean up old keys
        foreach (Transform child in m_Container)
        {
            if (child == m_KeyTemplate) continue;
            Destroy(child.gameObject);
        }

        // Instantiate current key list
        for (int i = 0; i < keyList.Count; i++)
        {
            ColorPalette.ColorName keyType = keyList[i];
            Transform keyTransform = Instantiate(m_KeyTemplate, m_Container);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);
            keyTransform.gameObject.SetActive(true);
            Image keyImage = keyTransform.Find("Image").GetComponent<Image>();
            keyImage.color = ColorPalette.GetColor32(keyType);
        }
    }

    public void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList)
    {
        UpdateVisual(keyList);
    }

}