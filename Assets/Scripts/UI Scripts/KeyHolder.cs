using System.Collections;
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
            KeyScript.KeyType keyType = keyList[i];
            Transform keyTransform = Instantiate(keyTemplate, container);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(50 * i, 0);
            keyTransform.gameObject.SetActive(true);
            Image keyImage = keyTransform.Find("Image").GetComponent<Image>();
            keyImage.color = KeyScript.GetKeyColor32(keyType);
            // switch (keyType)
            // {
            //     default:
            //     case KeyScript.KeyType.Blue: keyImage.sprite = myKeySprites[0]; break;
            //     case KeyScript.KeyType.Brown: keyImage.sprite = myKeySprites[1]; break;
            //     case KeyScript.KeyType.Gray: keyImage.sprite = myKeySprites[2]; break;
            //     case KeyScript.KeyType.Green: keyImage.sprite = myKeySprites[3]; break;
            //     case KeyScript.KeyType.Purple: keyImage.sprite = myKeySprites[4]; break;
            //     case KeyScript.KeyType.Red: keyImage.sprite = myKeySprites[5]; break;
            //     case KeyScript.KeyType.Silver: keyImage.sprite = myKeySprites[6]; break;
            //     case KeyScript.KeyType.Teal: keyImage.sprite = myKeySprites[7]; break;
            //     case KeyScript.KeyType.Violet: keyImage.sprite = myKeySprites[8]; break;
            //     case KeyScript.KeyType.White: keyImage.sprite = myKeySprites[9]; break;


            // }
            //keyImage.sprite = GameObject.Find("KeyRed").GetComponent<Sprite>();
        }
    }

}
