using System.Collections.Generic;

public interface IKeyGrabberEvents
{
    void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList); // Invoked from KeyGrabber.cs
    void OnKeyAdded(object sender, ColorPalette.ColorName keyType); // Invoked from KeyGrabber.cs
    void OnKeyRemoved(object sender, ColorPalette.ColorName keyType); // Invoked from KeyGrabber.cs
}