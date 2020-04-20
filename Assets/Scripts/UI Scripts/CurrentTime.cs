using System;
using TMPro;
using UnityEngine;

public class CurrentTime : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = String.Format("{0:0.00}", Time.timeSinceLevelLoad);
        String.Format("{0:0.00}", Time.timeSinceLevelLoad);

    }
}
