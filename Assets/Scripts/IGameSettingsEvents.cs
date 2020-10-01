public interface IGameSettingsEvents
{
    void OnGameSettingsChanged(object sender, GameSettings.GameSettingsInfo gameSettingsInfo); // Invoked from GameSettings.cs
}
