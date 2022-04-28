using UnityEngine.SceneManagement;

public interface ISceneHandlerEvents
{
    void OnSceneLoad(Scene arg0, LoadSceneMode arg1); // TODO: Currently being invoked from SceneManager.sceneLoaded event. I have to change it so it's invoked from SceneHandler.OnSceneLoadEvent.
}
