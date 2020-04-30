using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [Header("Level Info")]
    [SerializeField] private string m_LevelName;
    [SerializeField] private SceneHandler.Scene m_Scene;

    [Header("Node Destinations")]
    [SerializeField] private GameObject upDestination;
    [SerializeField] private GameObject downDestination;
    [SerializeField] private GameObject leftDestination;
    [SerializeField] private GameObject rightDestination;


    [Header("Dependencies")]
    [SerializeField] private GameObject myPlayer;

    [SerializeField] private InputHandler myInputHandler;

    private bool canMove;




    IEnumerator DoUp()
    {
        yield return new WaitForSeconds(1 / 60);
        while (myPlayer.transform.position != upDestination.transform.position)
        {
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, upDestination.transform.position, 8f * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator DoDown()
    {
        yield return new WaitForSeconds(1 / 60);
        while (myPlayer.transform.position != downDestination.transform.position)
        {
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, downDestination.transform.position, 8f * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator DoLeft()
    {
        yield return new WaitForSeconds(1 / 60);
        while (myPlayer.transform.position != leftDestination.transform.position)
        {
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, leftDestination.transform.position, 8f * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator DoRight()
    {
        yield return new WaitForSeconds(1 / 60);
        while (myPlayer.transform.position != rightDestination.transform.position)
        {
            myPlayer.transform.position = Vector3.MoveTowards(myPlayer.transform.position, rightDestination.transform.position, 8f * Time.deltaTime);
            yield return null;
        }

    }
}
