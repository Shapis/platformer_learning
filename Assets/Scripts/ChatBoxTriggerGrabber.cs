using System;
using UnityEngine;

public class ChatBoxTriggerGrabber : MonoBehaviour, IChatBoxTriggerEvents
{
    public event EventHandler<ChatBoxTrigger> OnChatBoxTriggerEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ChatBoxTrigger chatBoxTrigger = other.gameObject.GetComponent<ChatBoxTrigger>();
        if (chatBoxTrigger != null && chatBoxTrigger.Tangible)
        {
            chatBoxTrigger.Tangible = false;
            OnChatBoxTriggerCollided(this, chatBoxTrigger);
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     ChatBoxTrigger chatBoxTrigger = other.gameObject.GetComponent<ChatBoxTrigger>();
    //     if (chatBoxTrigger != null && chatBoxTrigger.Tangible)
    //     {
    //         //chatBoxTrigger.Tangible = false;
    //         OnChatBoxTriggerCollided(this, EventArgs.Empty);
    //     }
    // }

    public void OnChatBoxTriggerCollided(object sender, ChatBoxTrigger chatBoxTrigger)
    {
        OnChatBoxTriggerEvent?.Invoke(this, chatBoxTrigger);
    }
}
