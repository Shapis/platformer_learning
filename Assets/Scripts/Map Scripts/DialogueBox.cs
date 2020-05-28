using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private PlayerNodeMovement m_PlayerNodeMovement;
    [SerializeField] PopupMenuController m_PopUpMenuController;

    private DialogueHandler myDialogueHandler;

    private void Start()
    {
        m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReached;
        m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDeparted;
        OnDestinationNodeReached(this, m_PlayerNodeMovement.GetCurrentNode().gameObject);
    }

    private void OnDestinationNodeReached(object sender, GameObject e)
    {
        m_PopUpMenuController.OpenMenu();

        if (e.GetComponent<Dialogue>() != null)
        {
            myDialogueHandler = new DialogueHandler();
            myDialogueHandler.InitializeDialogue(e.GetComponent<Dialogue>());
            StopAllCoroutines();
            StartCoroutine(TypeSentence(myDialogueHandler.NextSentence()));
        }
    }
    private void OnDestinationNodeDeparted(object sender, GameObject e)
    {
        m_PopUpMenuController.CloseMenu();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }

}
