using UnityEngine;

public class KeyDoorScript : MonoBehaviour
{
    [SerializeField] private KeyScript.KeyType m_KeyType;

    [SerializeField] private Animator m_Animator;

    [SerializeField] private BoxCollider2D[] m_BoxColliders;

    [SerializeField] private GameObject m_GateKeyType;

    private bool isOpenPermanently = false;

    private void Awake()
    {
        m_GateKeyType.GetComponent<SpriteRenderer>().color = KeyScript.GetKeyColor32(m_KeyType);
        // myKey = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        // myKey.color = KeyScript.GetKeyColor32(m_KeyType);
    }


    public KeyScript.KeyType GetKeyType()
    {
        return m_KeyType;
    }

    public void OpenDoor()
    {
        if (!isOpenPermanently)
        {
            m_Animator.SetBool("isOpen", true);
            LeanTween.delayedCall(0.3f, DisableDoorColliders);
            LeanTween.scale(m_GateKeyType, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
            LeanTween.moveLocal(m_GateKeyType, new Vector3(0f, 1.8f, 0f), 0.7f);
            // Destroy(keySprite.gameObject, 0.3f);
        }
    }

    public void CloseDoor()
    {
        if (!isOpenPermanently)
        {
            m_Animator.SetBool("isOpen", false);
            LeanTween.delayedCall(0.3f, EnableDoorColliders);
            LeanTween.scale(m_GateKeyType, new Vector3(0.6f, 0.6f, 0.6f), 0.3f);
            LeanTween.moveLocal(m_GateKeyType, new Vector3(0f, -0.214f, 0f), 0.7f);
        }
    }

    public void OpenDoorPermanently()
    {
        OpenDoor();
        isOpenPermanently = true;
    }

    private void DisableDoorColliders()
    {
        foreach (var o in m_BoxColliders)
        {
            o.enabled = false;
        }
    }

    private void EnableDoorColliders()
    {
        foreach (var o in m_BoxColliders)
        {
            o.enabled = true;
        }
    }
}

