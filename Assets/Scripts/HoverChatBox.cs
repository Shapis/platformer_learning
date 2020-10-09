using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HoverChatBox : MonoBehaviour, IHoverChatBoxEvents
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Transform m_Player;
    private float textOffset;

    public event EventHandler OnHoverChatBoxEndsEvent;
    public event EventHandler OnHoverChatBoxNextEvent;
    public event EventHandler OnHoverChatBoxStartsEvent;
    private DialogueHandler myDialogueHandler = new DialogueHandler();
    private bool currentlyTyping = false;
    private Coroutine typeSentenceCoroutine;
    //private float timer = 0;



    private void Start()
    {
        m_Player = GameObject.Find("Player").transform;
        dialogueText.text = "";
        MoveToAbovePlayer();
    }

    private void LateUpdate()
    {
        MoveToAbovePlayer();
    }

    // TODO: This needs to adapt to the screen height.
    private void MoveToAbovePlayer()
    {
        Vector3 offset = new Vector3(0f, textOffset, 0f);
        Vector3 abc = new Vector3();
        GetComponent<RectTransform>().position = Vector3.SmoothDamp(GetComponent<RectTransform>().position,
        Camera.main.WorldToScreenPoint(m_Player.transform.position) + offset,
        ref abc,
        0.02f);
    }

    public void OnHoverChatBoxEnds(object sender, EventArgs e)
    {
        OnHoverChatBoxEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnHoverChatBoxNext(object sender, EventArgs e)
    {
        OnHoverChatBoxNextEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnHoverChatBoxStarts(object sender, EventArgs e)
    {
        OnHoverChatBoxStartsEvent?.Invoke(this, EventArgs.Empty);
    }

    internal void AddToHoverChatBox(object sender, Dialogue dialogue)
    {
        myDialogueHandler.AddToQueue(dialogue);

        if (!currentlyTyping)
        {
            NextSentence();
        }
    }

    private void NextSentence()
    {
        currentlyTyping = true;

        if (myDialogueHandler.GetSentencesCount() > 0)
        {
            if (typeSentenceCoroutine != null)
            {
                StopCoroutine(typeSentenceCoroutine);
            }
            typeSentenceCoroutine = StartCoroutine(TypeSentence(myDialogueHandler.NextSentence()));
        }
        else if (myDialogueHandler.GetSentencesCount() == 0)
        {
            EndHoverChatBox();
        }
    }

    private void EndHoverChatBox()
    {
        typeSentenceCoroutine = StartCoroutine(UntypeSentence());
    }

    private IEnumerator UntypeSentence()
    {
        while (dialogueText.text != "")
        {
            dialogueText.text = dialogueText.text.Remove(dialogueText.text.Length - 1);
            yield return new WaitForSeconds(0.01f);
        }

        if (myDialogueHandler.GetSentencesCount() > 0)
        {
            NextSentence();
        }
        currentlyTyping = false;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        ResizeDialogueBoxAccordingToTextSize(sentence);
        float readingTime = 0f;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            readingTime += 0.04f;
            yield return new WaitForSeconds(0.02f);
        }
        if (readingTime > 3f)
        {
            yield return new WaitForSeconds(readingTime);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        NextSentence();
    }

    private void ResizeDialogueBoxAccordingToTextSize(string sentence)
    {
        int numberOfLines = 0;
        float letterHeight = 17.83f;
        float padding = 0f;

        dialogueText.text = sentence;
        dialogueText.ForceMeshUpdate();
        numberOfLines = dialogueText.textInfo.lineCount;
        dialogueText.text = "";
        dialogueText.ForceMeshUpdate();

        float textHeight = 0;
        switch (numberOfLines)
        {
            case 0: textHeight = padding + 2 * letterHeight; break;
            case 1: textHeight = padding + 2 * letterHeight; break;
            default: textHeight = padding + numberOfLines * letterHeight; break;
        }

        textOffset = textHeight;
    }
}
