using UnityEngine;


// https://lospec.com/palette-list/a64 I'm using this Color Palette
public static class ColorPalette
{
    public enum ColorName
    {
        Black,
        Blue,
        Brown,
        DarkBrown,
        DarkGreen,
        Gray,
        Green,
        Ice,
        Purple,
        Red,
        Salmon,
        Silver,
        Teal,
        Violet,
        White,
        Yellow,
    }

    public static Color32 GetColor32(ColorPalette.ColorName myColor)
    {
        Color32 myColor32 = new Color32();
        switch (myColor)
        {
            default:
            case ColorPalette.ColorName.Black:
                myColor32 = new Color32(0, 0, 0, 255);
                break;
            case ColorPalette.ColorName.Blue:
                myColor32 = new Color32(49, 58, 145, 255);
                break;
            case ColorPalette.ColorName.Brown:
                myColor32 = new Color32(146, 86, 43, 255);
                break;
            case ColorPalette.ColorName.DarkBrown:
                myColor32 = new Color32(76, 52, 53, 255);
                break;
            case ColorPalette.ColorName.DarkGreen:
                myColor32 = new Color32(80, 148, 80, 255);
                break;
            case ColorPalette.ColorName.Gray:
                myColor32 = new Color32(72, 84, 84, 255);
                break;
            case ColorPalette.ColorName.Green:
                myColor32 = new Color32(156, 204, 71, 255);
                break;
            case ColorPalette.ColorName.Ice:
                myColor32 = new Color32(156, 171, 177, 255);
                break;
            case ColorPalette.ColorName.Purple:
                myColor32 = new Color32(118, 85, 162, 255);
                break;
            case ColorPalette.ColorName.Red:
                myColor32 = new Color32(177, 72, 99, 255);
                break;
            case ColorPalette.ColorName.Salmon:
                myColor32 = new Color32(205, 147, 115, 255);
                break;
            case ColorPalette.ColorName.Silver:
                myColor32 = new Color32(128, 128, 120, 255);
                break;
            case ColorPalette.ColorName.Teal:
                myColor32 = new Color32(143, 191, 213, 255);
                break;
            case ColorPalette.ColorName.Violet:
                myColor32 = new Color32(131, 133, 207, 255);
                break;
            case ColorPalette.ColorName.White:
                myColor32 = new Color32(237, 230, 200, 255);
                break;
            case ColorPalette.ColorName.Yellow:
                myColor32 = new Color32(187, 200, 64, 255);
                break;
        }
        return myColor32;
    }
}
