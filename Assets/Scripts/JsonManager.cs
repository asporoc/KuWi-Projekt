using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
    private string jsonFilePath;
    private UserData userData;

    void Start()
    {
        jsonFilePath = Path.Combine(Application.streamingAssetsPath, "text_data.json");
        LoadJson();
        QueryUsers();
    }

    void LoadJson()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);
            userData = JsonUtility.FromJson<UserData>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found at " + jsonFilePath);
        }
    }

    void QueryUsers()
    {
        if (userData != null && userData.users != null)
        {
            foreach (User user in userData.users)
            {
                Debug.Log($"Name: {user.name}\nText: {user.text}\n");
            }
        }
    }
}
