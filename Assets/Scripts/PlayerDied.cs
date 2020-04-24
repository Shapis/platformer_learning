using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDied
{
    private GameObject myPlayer;
    private void Start()
    {
        myPlayer = GameObject.Find("Player");
    }
    public void Died()
    {
        myPlayer.GetComponent<Animator>().SetBool("isJumping", false);
        myPlayer.GetComponent<Animator>().SetBool("isAirbourne", false);
        myPlayer.GetComponent<Animator>().SetBool("isDead", true);
        myPlayer.GetComponent<PlayerMovement>().enabled = false;

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
