using System;
using UnityEngine;

public class KeyDoorScript : MonoBehaviour, IKeyDoorEvents
{
    [SerializeField] private KeyScript.KeyType m_KeyType;

    [SerializeField] private Animator m_Animator;

    [SerializeField] private BoxCollider2D[] m_BoxColliders;

    [SerializeField] private GameObject m_GateKeyType;

    private bool isOpenPermanently = false;

    public event EventHandler OnDoorOpenEvent;

    public event EventHandler OnDoorCloseEvent;

    public event EventHandler OnDoorOpenPermanentlyEvent;

    private void Awake()
    {
        m_GateKeyType.GetComponent<SpriteRenderer>().color = KeyScript.GetKeyColor32(m_KeyType);
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
            OnDoorOpen();
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
            OnDoorClose();
        }
    }

    public void OpenDoorPermanently()
    {
        OpenDoor();
        isOpenPermanently = true;
        OnDoorOpenPermanently();
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

    private void OnDoorOpen()
    {
        OnDoorOpenEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnDoorClose()
    {
        OnDoorCloseEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnDoorOpenPermanently()
    {
        OnDoorOpenPermanentlyEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDoorClose(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}

