using System;

public interface IDialogueBoxEvents
{
    void OnDialogueBoxStarts(object sender, EventArgs e);// Invoked from DialogueBox.cs
    void OnDialogueBoxNext(object sender, EventArgs e); // Invoked from DialogueBox.cs
    void OnDialogueBoxEnds(object sender, EventArgs e); // Invoked from DialogueBox.cs
    void OnDialogueBoxTextCompletes(object sender, EventArgs e); // Invoked from DialogueBox.cs
}