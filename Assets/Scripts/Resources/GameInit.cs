using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}
