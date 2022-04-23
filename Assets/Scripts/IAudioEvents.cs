public interface IAudioEvents
{
    void OnSfxPlay(object sender, System.EventArgs e); // Invoked from AudioHandler.cs
    void OnSfxStop(object sender, System.EventArgs e); // Invoked from AudioHandler.cs
    void OnSfxPause(object sender, System.EventArgs e); // Invoked from AudioHandler.cs

    void OnMusicPlay(object sender, System.EventArgs e); // Invoked from AudioHandler.cs

    void OnMusicStop(object sender, System.EventArgs e); // Invoked from AudioHandler.cs

    void OnMusicPause(object sender, System.EventArgs e); // Invoked from AudioHandler.cs
}
