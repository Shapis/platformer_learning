using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        keyHolder.OnKeysChangedEvent += OnKeysChanged;

    }


    private void OnKeysChanged(object sender, System.EventArgs e)
    {

        UpdateVisual();

    }


    private void UpdateVisual()
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
            Transform keyTransform = Instantiate(keyTemplate, container);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
            keyTransform.gameObject.SetActive(true);
        }
    }

}
