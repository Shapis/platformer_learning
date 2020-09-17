using System;

public interface IHoverChatBoxEvents
{
    void OnHoverChatBoxStarts(object sender, EventArgs e); // Invoked from HoverChatBox.cs
    void OnHoverChatBoxNext(object sender, EventArgs e); // Invoked from HoverChatBox.cs
    void OnHoverChatBoxEnds(object sender, EventArgs e); // Invoked from HoverChatBox.cs
}
