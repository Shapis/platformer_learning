public interface IKeyDoorEvents
{
    void OnDoorOpen(object sender, System.EventArgs e);
    void OnDoorClose(object sender, System.EventArgs e);
    void OnDoorOpenPermanently(object sender, System.EventArgs e);
}
