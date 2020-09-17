using System;
using UnityEngine;

public class ChatBoxTrigger : BaseItem, IChatBoxTriggerEvents
{
    private ChatBoxTriggerGrabber m_ChatBoxTriggerGrabber;
    private InputHandler m_InputHandler;
    private HoverChatBox m_HoverChatBox;
    private ChatBoxTrigger currentChatBoxTrigger;

    // Awake instead of Start because of https://forum.unity.com/threads/why-does-gameobject-find-getcomponent-not-work-when-a-new-scene-is-loaded.428460/
    // I was having issues with the m_DialogueBox not being found properly after a scene reload, need to investigate this.
    void Awake()
    {
        m_ChatBoxTriggerGrabber = GameObject.Find("Player").GetComponent<ChatBoxTriggerGrabber>();
        m_HoverChatBox = GameObject.Find("Canvas_Overlay").GetComponentInChildren<HoverChatBox>();

        m_ChatBoxTriggerGrabber.OnChatBoxTriggerEvent += OnChatBoxTriggerCollided;

        m_InputHandler = GameObject.Find("InputHandler").GetComponent<InputHandler>();
        //m_InputHandler.OnCancelPressedEvent += (s, e) => m_DialogueBox.EndDialogueBox();

        // m_HoverChatBox.OnHoverChatBoxStartsEvent += OnDialogueBoxStarts;
        // m_HoverChatBox.OnHoverChatBoxEndsEvent += OnDialogueBoxEnds;
    }

    public void OnChatBoxTriggerCollided(object sender, ChatBoxTrigger chatBoxTrigger)
    {
        if (chatBoxTrigger == this)
        {
            currentChatBoxTrigger = chatBoxTrigger;
            m_HoverChatBox.AddToHoverChatBox(this, gameObject.GetComponent<Dialogue>());
        }
    }
}
