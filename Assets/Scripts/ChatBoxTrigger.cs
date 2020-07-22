using System;
using UnityEngine;

public class ChatBoxTrigger : BaseItem, IChatBoxTriggerEvents, IDialogueBoxEvents
{
    private ChatBoxTriggerGrabber m_ChatBoxTriggerGrabber;
    private InputHandler m_InputHandler;
    private DialogueBox m_DialogueBox;
    private ChatBoxTrigger currentChatBoxTrigger;

    private int myEscapeSwitch = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_ChatBoxTriggerGrabber = GameObject.Find("Player").GetComponent<ChatBoxTriggerGrabber>();

        m_DialogueBox = GameObject.Find("Canvas_Overlay").GetComponentInChildren<DialogueBox>();
        m_DialogueBox.gameObject.GetComponent<PopupMenuController>().gameObject.transform.localScale = new Vector3(0, 0, 0);

        m_ChatBoxTriggerGrabber.OnChatBoxTriggerEvent += OnChatBoxTriggerCollided;

        m_InputHandler = GameObject.Find("InputHandler").GetComponent<InputHandler>();
        //m_InputHandler.OnCancelPressedEvent += (s, e) => m_DialogueBox.EndDialogueBox();

        m_DialogueBox.OnDialogueBoxStartsEvent += OnDialogueBoxStarts;
        m_DialogueBox.OnDialogueBoxEndsEvent += OnDialogueBoxEnds;
    }

    public void OnChatBoxTriggerCollided(object sender, ChatBoxTrigger chatBoxTrigger)
    {
        if (chatBoxTrigger == this)
        {
            currentChatBoxTrigger = chatBoxTrigger;
            m_DialogueBox.StartDialogueBox(this, gameObject.GetComponent<Dialogue>());
        }
    }

    public void OnDialogueBoxStarts(object sender, EventArgs e)
    {
        if (currentChatBoxTrigger == this)
        {
            m_InputHandler.OnCancelPressedEvent += DisableEscape;
            m_DialogueBox.OnDialogueBoxStartsEvent -= OnDialogueBoxStarts;
            GameHandler.Pause();
        }
    }

    private void DisableEscape(object sender, EventArgs e)
    {
        myEscapeSwitch = 1;
        m_DialogueBox.EndDialogueBox();
    }

    public void OnDialogueBoxEnds(object sender, EventArgs e)
    {
        if (currentChatBoxTrigger == this)
        {
            m_DialogueBox.OnDialogueBoxEndsEvent -= OnDialogueBoxEnds;
            if (myEscapeSwitch == 0)
            {
                GameHandler.Resume();
            }
            m_InputHandler.OnCancelPressedEvent -= DisableEscape;
            myEscapeSwitch = 0;
        }
    }


    public void OnDialogueBoxNext(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void OnDialogueBoxTextCompletes(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
