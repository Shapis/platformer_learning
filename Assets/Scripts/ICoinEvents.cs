using System.Collections.Generic;

public interface ICoinEvents
{
    void OnCoinsChanged(object sender, List<ColorPalette.ColorName> coinList); // Invoked from CoinGrabber.cs
}
