public interface IPressurePlateEvents
{
    void OnPressurePlateActivated(object sender, ColorPalette.ColorName pressurePlateColor);
    void OnPressurePlateDeactivated(object sender, ColorPalette.ColorName pressurePlateColor);
}
