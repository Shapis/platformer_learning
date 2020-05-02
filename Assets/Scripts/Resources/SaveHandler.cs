using UnityEngine;

public static class SaveHandler<T>
{
    public enum SaveFileName
    {
        levelCompletionInfo,
        currentPlayerNode,
        audioSettings,
    }

    // Saves the object of generic type <T> as the file of the name of enum SaveFileName.Whatever
    public static void Save(T myObject, SaveFileName saveFileName)
    {
        string json = JsonUtility.ToJson(myObject);
        PlayerPrefs.SetString(saveFileName.ToString(), json);
        PlayerPrefs.Save();
    }

    // Loads the json with the name of enum SaveFileName.Whatever converts it back to the object type <T> and returns it as an object of type <T>
    public static T Load(SaveFileName saveFileName)
    {
        string json = PlayerPrefs.GetString(saveFileName.ToString());
        return JsonUtility.FromJson<T>(json);
    }
}
