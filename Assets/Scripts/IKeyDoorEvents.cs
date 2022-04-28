public interface IKeyDoorEvents
{
    void OnDoorOpen(object sender, System.EventArgs e); // Invoked from KeyDoor.cs
    void OnDoorClose(object sender, System.EventArgs e); // Invoked from KeyDoor.cs
    void OnDoorOpenPermanently(object sender, System.EventArgs e); // Invoked from KeyDoor.cs
}
