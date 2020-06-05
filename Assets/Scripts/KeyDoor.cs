using System;
using UnityEngine;

public class KeyDoor : MonoBehaviour, IKeyDoorEvents
{
    [Header("Dependencies")]
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject m_GateKeyGameObject;
    [SerializeField] private BoxCollider2D[] m_BoxColliders;

    [Header("Settings")]
    [SerializeField] private ColorPalette.ColorName m_KeyType;
    public ColorPalette.ColorName GetKeyType()
    {
        return m_KeyType;
    }

    public event EventHandler OnDoorOpenEvent;
    public event EventHandler OnDoorCloseEvent;
    public event EventHandler OnDoorOpenPermanentlyEvent;
    private bool isOpenPermanently = false;

    private void Start()
    {
        m_GateKeyGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.GetColor32(m_KeyType);
    }

    public void OpenDoor()
    {
        if (!isOpenPermanently)
        {
            m_Animator.SetBool("isOpen", true);
            //LeanTween.delayedCall(0.3f, DisableDoorColliders);
            // LeanTween.scale(m_GateKeyGameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
            // LeanTween.moveLocal(m_GateKeyGameObject, new Vector3(0f, 1.8f, 0f), 0.7f);
            // Destroy(keySprite.gameObject, 0.3f);
            OnDoorOpen(this, EventArgs.Empty);
        }
    }

    public void CloseDoor()
    {
        if (!isOpenPermanently)
        {
            m_Animator.SetBool("isOpen", false);
            //LeanTween.delayedCall(0.3f, EnableDoorColliders);
            // LeanTween.scale(m_GateKeyGameObject, new Vector3(0.6f, 0.6f, 0.6f), 0.3f);
            // LeanTween.moveLocal(m_GateKeyGameObject, new Vector3(0f, -0.214f, 0f), 0.7f);
            OnDoorClose(this, EventArgs.Empty);
        }
    }

    public void OpenDoorPermanently()
    {
        OpenDoor();
        isOpenPermanently = true;
        OnDoorOpenPermanently(this, EventArgs.Empty);
    }


    private void EnableDoorColliders()
    {
        foreach (var o in m_BoxColliders)
        {
            o.enabled = true;
        }
    }
    private void DisableDoorColliders()
    {
        foreach (var o in m_BoxColliders)
        {
            o.enabled = false;
        }
    }

    public void OnDoorClose(object sender, EventArgs e)
    {
        OnDoorCloseEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDoorOpen(object sender, EventArgs e)
    {
        OnDoorOpenEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDoorOpenPermanently(object sender, EventArgs e)
    {
        OnDoorOpenPermanentlyEvent?.Invoke(this, EventArgs.Empty);
    }
}
