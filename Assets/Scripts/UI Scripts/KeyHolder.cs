using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHolder : MonoBehaviour
{
    private KeyHolderScript keyHolder;

    private Transform container;
    private Transform keyTemplate;

    private void Awake()
    {
        container = transform.Find("ContainerKeys");
        keyTemplate = container.Find("KeyTemplate");
        keyTemplate.gameObject.SetActive(false);
        keyHolder = GameObject.Find("Player").GetComponent<KeyHolderScript>();
        //keyHolder = GameObject.Find("Player").GetComponent<KeyHolder>();
    }
    private void Start()
    {
        keyHolder.OnKeysChangedEvent += UpdateVisual;
    }

    private void UpdateVisual(object sender, EventArgs e)
    {

        // Clean up old keys
        foreach (Transform child in container)
        {
            if (child == keyTemplate) continue;
            Destroy(child.gameObject);
        }

        List<KeyScript.KeyType> keyList = keyHolder.GetKeyList();

        // Instantiate current key list

        for (int i = 0; i < keyList.Count; i++)
        {
            KeyScript.KeyType keyType = keyList[i];
            Transform keyTransform = Instantiate(keyTemplate, container);

            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(250 * i, 0);

            keyTransform.gameObject.SetActive(true);
            Image keyImage = keyTransform.Find("Image").GetComponent<Image>();
            keyImage.color = KeyScript.GetKeyColor32(keyType);
        }
    }

}