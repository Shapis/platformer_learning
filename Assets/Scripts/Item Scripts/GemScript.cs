using UnityEngine.SceneManagement;
using UnityEngine;

public class GemScript : MonoBehaviour
{


    private void Awake()
    {

        Debug.Log(SceneHandler.GetSceneEnum());

        //SceneHandler.Load(SceneHandler.GetSceneEnum());

    }

}
