using UnityEngine.SceneManagement;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public bool Tangible { get; set; } = true;

    private void Awake()
    {

        //Debug.Log(SceneHandler.GetActiveSceneEnum());

        //SceneHandler.Load(SceneHandler.GetSceneEnum());

    }

}
