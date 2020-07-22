using System;
using System.Collections.Generic;

public class DialogueHandler
{
    private readonly Queue<string> sentences = new Queue<string>();

    public int GetSentencesCount()
    {
        return sentences.Count;
    }

    public event EventHandler OnInitializeDialogueEvent;
    public event EventHandler OnNextSentenceEvent;
    public event EventHandler OnEndDialogueEvent;

    public void InitializeDialogue(Dialogue dialogue)
    {
        OnInitializeDialogueEvent?.Invoke(this, EventArgs.Empty);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public string NextSentence()
    {
        OnNextSentenceEvent?.Invoke(this, EventArgs.Empty);

        if (sentences.Count == 0)
        {
            return EndDialogue();
        }

        return sentences.Dequeue();
    }

    private string EndDialogue()
    {
        OnEndDialogueEvent?.Invoke(this, EventArgs.Empty);

        // Ideally you want to avoid this outcome being possible by checking how many sentences are left with GetSentencesCount()
        // And coding the proper behavior for what happens when you're on the last sentence of your dialogue instead of defaulting to this.
        return "Dialogue ended";
    }
}
