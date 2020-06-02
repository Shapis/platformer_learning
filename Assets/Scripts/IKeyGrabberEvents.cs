using System.Collections.Generic;

public interface IKeyGrabberEvents
{
    void OnKeysChanged(object sender, List<ColorPalette.ColorName> keyList);
}