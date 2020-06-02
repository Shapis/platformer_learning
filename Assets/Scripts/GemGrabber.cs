using System;
using UnityEngine;

public class GemGrabber : MonoBehaviour {
    public event EventHandler OnLevelEndsEvent;
    private void OnTriggerEnter2D (Collider2D other) {

        if (other.gameObject.GetComponent<GemScript> () != null) {
            if (other.gameObject.GetComponent<GemScript> ().Tangible) {
                OnLevelEnd ();
                other.gameObject.GetComponent<GemScript> ().Tangible = false;
                LevelCompletion levelCompletion = new LevelCompletion ();
                levelCompletion.LevelHasBeenCompleted ();
            }
        }
    }

    private void OnLevelEnd () {
        OnLevelEndsEvent?.Invoke (this, EventArgs.Empty);
    }
}