public interface IPressurePlateEvents
{
    void OnPressurePlateActivated(object sender, KeyDoor[] matchingColorDoorList);
    void OnPressurePlateDeactivated(object sender, KeyDoor[] matchingColorDoorList);
}
