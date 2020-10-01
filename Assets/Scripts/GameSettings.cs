using System;
using UnityEngine;

public class GameSettings : MonoBehaviour, IGameSettingsEvents
{
    public void OnSettingsChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    [System.Serializable]
    public struct GameSettingsInfo
    {
        public bool rightHandedMode;
        public float joystickSize;
        public float volume;
    }
}
