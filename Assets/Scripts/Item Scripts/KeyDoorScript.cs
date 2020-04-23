using UnityEngine;

public class KeyDoorScript : MonoBehaviour
{
    [SerializeField] private KeyScript.KeyType keyType;

    [SerializeField] private Animator animator;

    [SerializeField] private BoxCollider2D[] boxColliders;

    [SerializeField] private Sprite[] myKeySprites;

    private SpriteRenderer keySprite;

    private void Awake()
    {
        // keySprite = GameObject.Find("GateKeyType").GetComponent<SpriteRenderer>();

        // switch (keyType)
        // {
        //     default:
        //     case KeyScript.KeyType.Blue: keySprite.sprite = myKeySprites[0]; break;
        //     case KeyScript.KeyType.Brown: keySprite.sprite = myKeySprites[1]; break;
        //     case KeyScript.KeyType.Gray: keySprite.sprite = myKeySprites[2]; break;
        //     case KeyScript.KeyType.Green: keySprite.sprite = myKeySprites[3]; break;
        //     case KeyScript.KeyType.Purple: keySprite.sprite = myKeySprites[4]; break;
        //     case KeyScript.KeyType.Silver: keySprite.sprite = myKeySprites[5]; break;
        //     case KeyScript.KeyType.Teal: keySprite.sprite = myKeySprites[6]; break;
        //     case KeyScript.KeyType.Violet: keySprite.sprite = myKeySprites[7]; break;
        //     case KeyScript.KeyType.White: keySprite.sprite = myKeySprites[8]; break;

        // }
        // //keyImage.sprite = GameObject.Find("KeyRed").GetComponent<Sprite>();
    }


    public KeyScript.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        //gameObject.SetActive(false);
        gameObject.GetComponent<Collider2D>();
        animator.SetBool("isOpen", true);
        LeanTween.delayedCall(0.3f, DisableDoorColliders);
        // LeanTween.scale(keySprite.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.3f);
        // LeanTween.moveLocal(keySprite.gameObject, new Vector3(0f, 1.5f, 0f), 0.3f);
        // Destroy(keySprite.gameObject, 0.3f);


    }

    private void DisableDoorColliders()
    {
        foreach (var o in boxColliders)
        {
            o.enabled = false;
        }
    }
}

