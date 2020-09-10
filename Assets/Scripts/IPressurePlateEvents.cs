public interface IPressurePlateEvents
{
    void OnPressurePlateActivated(object sender, ColorPalette.ColorName pressurePlateColor);    // Invoked from PressurePlate.cs 
    void OnPressurePlateDeactivated(object sender, ColorPalette.ColorName pressurePlateColor);  // Invoked from PressurePlate.cs 
}
