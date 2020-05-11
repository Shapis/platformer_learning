using UnityEngine;

public class WorldMapUI : MonoBehaviour
{
    [SerializeField] private InputHandler m_InputHandler;
    void Awake()
    {
        m_InputHandler.OnCancelPressedEvent += (a, b) => SceneHandler.Load(SceneHandler.Scene.MainMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
