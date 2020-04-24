using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied
{
    public void Died()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isJumping", false);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isAirbourne", false);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isDead", true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;

        if (GameObject.Find("UnderneathTrigger") != null)
        {
            GameObject.Find("UnderneathTrigger").gameObject.SetActive(false);
        }

        Debug.Log("Player died");

        SceneHandler.ReloadCurrentScene();
    }
    public void Drowned()
    {
        PlayerDied myPlayerDied = new PlayerDied();

        myPlayerDied.Died();

        Debug.Log("Player drowned.");
    }
}
