using System;

public interface IDialogueBoxEvents
{
    void OnDialogueBoxStarts(object sender, EventArgs e);
    void OnDialogueBoxNext(object sender, EventArgs e);
    void OnDialogueBoxEnds(object sender, EventArgs e);
    void OnDialogueBoxTextCompletes(object sender, EventArgs e);
}