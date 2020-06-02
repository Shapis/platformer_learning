using System;
using System.Collections;
using UnityEngine;

public static class DelayHandler
{
    public static IEnumerator DelayAction(float delayInSeconds, Action myAction)
    {
        yield return new WaitForSeconds(delayInSeconds);
        myAction();
    }
}
