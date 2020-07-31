using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeTrigger : MonoBehaviour
{
    [SerializeField] private PlayerNodeMovement m_PlayerNodeMovement;
    [SerializeField] private DialogueBox m_DialogueBox;
    private void Awake()
    {
        m_PlayerNodeMovement = GameObject.Find("Player").GetComponent<PlayerNodeMovement>();
        m_DialogueBox = GameObject.Find("Panel_DialogueBox").GetComponent<DialogueBox>();

    }
    private void Start()
    {
        m_PlayerNodeMovement.OnDestinationNodeReachedEvent += OnDestinationNodeReachedTrigger;
        m_PlayerNodeMovement.OnDestinationNodeDepartedEvent += OnDestinationNodeDepartedTrigger;
        if (m_PlayerNodeMovement.GetCurrentNode() == this.gameObject.GetComponent<Node>())
        {
            Debug.Log(this.gameObject.GetComponent<Node>());
            m_DialogueBox.StartDialogueBox(this, m_PlayerNodeMovement.GetCurrentNode().gameObject.GetComponent<Dialogue>());
        }
    }

    private void OnDestinationNodeReachedTrigger(object sender, GameObject e)
    {
        if (e == gameObject)
        {
            m_DialogueBox.StartDialogueBox(this, gameObject.GetComponent<Dialogue>());
        }
    }

    private void OnDestinationNodeDepartedTrigger(object sender, GameObject e)
    {
        if (e == gameObject)
        {
            m_DialogueBox.EndDialogueBox();
        }
    }
}
