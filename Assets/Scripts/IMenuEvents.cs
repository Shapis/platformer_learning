using System;

public interface IMenuEvents
{
    void OnMenuOpen(object sender, EventArgs e); // Invoked from PopUpMenuController.cs
    void OnMenuClose(object sender, EventArgs e); // Invoked from PopUpMenuController.cs
    void OnMenuButtonClick(object sender, EventArgs e);
    void OnMenuHover(object sender, EventArgs e);
}