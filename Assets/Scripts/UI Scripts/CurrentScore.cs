using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentScore : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Player").GetComponent<PlayerMovement>().currentScore += 1;
        gameObject.GetComponent<TextMeshProUGUI>().text = GameObject.Find("Player").GetComponent<PlayerMovement>().currentScore.ToString();
        //Debug.Log("Current Score Update test");




    }
}
