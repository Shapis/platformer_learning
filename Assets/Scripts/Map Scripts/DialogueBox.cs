using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour, IDialogueBoxEvents
{
    [Header("Dependencies")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private PopupMenuController m_PopUpMenuController;
    [SerializeField] private InputHandler m_InputHandler;
    [SerializeField] private Button m_DialogueBoxButton;
    private DialogueHandler myDialogueHandler;

    public event EventHandler OnDialogueBoxStartsEvent;
    public event EventHandler OnDialogueBoxNextEvent;
    public event EventHandler OnDialogueBoxEndsEvent;
    public event EventHandler OnDialogueBoxTextCompletesEvent;

    [Header("Settings")]
    [SerializeField] private bool m_CloseAfterDialogueOver = false;
    [SerializeField] private bool m_ClickAnywhereForNext = true;
    [SerializeField] private bool m_ClickOnDialogueBoxForNext = false;
    [SerializeField] private bool m_CanGoToNextBeforeTextIsDone = false;
    private bool CanGoToNextBeforeTextIsDoneSwitch = true;
    private bool dialogueBoxIsActive = false;

    private Coroutine typeSentenceCoroutine;

    private void Start()
    {
        m_InputHandler = GameObject.Find("InputHandler").GetComponent<InputHandler>();
        if (m_ClickOnDialogueBoxForNext)
        {
            m_DialogueBoxButton.onClick.AddListener(NextSentence);
        }
        m_InputHandler.OnMouseButtonLeftPressedEvent += OnMouseButtonLeftPressed;
    }

    private void OnMouseButtonLeftPressed(object sender, Vector2 e)
    {
        if (m_ClickAnywhereForNext && (myDialogueHandler != null))
        {
            NextSentence();
        }
    }

    public void StartDialogueBox(object sender, Dialogue myDialogue)
    {
        OnDialogueBoxStarts(this, EventArgs.Empty);
        m_PopUpMenuController.OpenMenu();
        dialogueBoxIsActive = true;
        ResizeDialogueBoxAccordingToTextSize(myDialogue.sentences);

        if (myDialogue != null)
        {
            myDialogueHandler = new DialogueHandler();
            myDialogueHandler.InitializeDialogue(myDialogue);
            NextSentence();
        }
    }

    private void ResizeDialogueBoxAccordingToTextSize(string[] sentence)
    {
        string longestString = "";
        int numberOfLines = 0;
        for (int i = 0; i < sentence.Length; i++)
        {
            if (longestString.Length < sentence[i].Length)
            {
                longestString = sentence[i];
            }
        }
        dialogueText.text = longestString;
        dialogueText.ForceMeshUpdate();
        numberOfLines = dialogueText.textInfo.lineCount;
        dialogueText.text = "";
        dialogueText.ForceMeshUpdate();

        float textHeight = 0;
        switch (numberOfLines)
        {
            case 0: textHeight = 11 + 2 * 22.5f; break;
            case 1: textHeight = 11 + 2 * 22.5f; break;
            default: textHeight = 11 + numberOfLines * 22.5f; break;
        }

        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, textHeight);
    }

    private void NextSentence()
    {
        if (m_CanGoToNextBeforeTextIsDone && dialogueBoxIsActive || CanGoToNextBeforeTextIsDoneSwitch && dialogueBoxIsActive)
        {
            CanGoToNextBeforeTextIsDoneSwitch = false;
            if (myDialogueHandler.GetSentencesCount() > 0)
            {
                if (typeSentenceCoroutine != null)
                {
                    StopCoroutine(typeSentenceCoroutine);
                }
                typeSentenceCoroutine = StartCoroutine(TypeSentence(myDialogueHandler.NextSentence()));
            }
            else if (myDialogueHandler.GetSentencesCount() == 0 && m_CloseAfterDialogueOver)
            {
                EndDialogueBox();
            }

        }
    }

    public void EndDialogueBox()
    {
        OnDialogueBoxEnds(this, EventArgs.Empty);
        m_PopUpMenuController.CloseMenu();
        dialogueBoxIsActive = false;
        CanGoToNextBeforeTextIsDoneSwitch = true;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (!GameHandler.isPaused)
            {
                yield return new WaitForSeconds(0.04f);
            }
            else
            {
                yield return new WaitForSecondsRealtime(0.04f);
            }
        }
        OnDialogueBoxTextCompletes(this, EventArgs.Empty);
        CanGoToNextBeforeTextIsDoneSwitch = true;
    }

    public void OnDialogueBoxStarts(object sender, EventArgs e)
    {
        OnDialogueBoxStartsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDialogueBoxEnds(object sender, EventArgs e)
    {
        OnDialogueBoxEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDialogueBoxNext(object sender, EventArgs e)
    {
        OnDialogueBoxNextEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnDialogueBoxTextCompletes(object sender, EventArgs e)
    {
        OnDialogueBoxTextCompletesEvent?.Invoke(this, EventArgs.Empty);
    }
}