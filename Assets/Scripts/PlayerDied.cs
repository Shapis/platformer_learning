using UnityEngine;

public class PlayerDied
{
    public void Died()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isAirbourne", false);
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isDead", true);
        GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        // Debug.Log("Player died");
        LeanTween.cancelAll();
        //SceneHandler.ReloadCurrentScene();
    }
    public void Drowned()
    {
        PlayerDied myPlayerDied = new PlayerDied();

        myPlayerDied.Died();

        Debug.Log("Player drowned.");
    }
}
