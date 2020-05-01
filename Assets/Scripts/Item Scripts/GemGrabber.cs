using System;
using UnityEngine;

public class GemGrabber : MonoBehaviour, ILevelEndedEvents
{

    [SerializeField] private PopupMenuController levelCompletedPopUp;

    //public SceneHandler.Scene MyNextScene;

    public event EventHandler OnLevelEndsEvent;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<GemScript>() != null)
        {
            if (other.gameObject.GetComponent<GemScript>().Tangible)
            {
                other.gameObject.GetComponent<GemScript>().Tangible = false;
                gameObject.GetComponent<PlayerScoreKeeper>().TotalTime = Time.timeSinceLevelLoad;
                LevelCompletion levelCompletion = new LevelCompletion();
                levelCompletion.LevelHasBeenCompleted();
                levelCompletedPopUp.OpenMenu();
                OnLevelEnd();
            }
        }
    }

    private void OnLevelEnd()
    {
        OnLevelEndsEvent?.Invoke(this, EventArgs.Empty);
    }

    public void OnLevelEnd(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
