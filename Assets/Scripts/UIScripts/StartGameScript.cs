using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System;
using System.IO;
using System.Collections;

public class StartGameScript : MonoBehaviour
{
    public InputField inputField;
    private JsonData jData;
    private string jsonName;
    void Awake()
    {
        jsonName = Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH;
        if (jData == null)
        {
            jData = new JsonData();
        }
        if (!File.Exists(jsonName))
        {
           File.Create(jsonName);
        }
    }
  
    public void StartNewGame()
    {
        if (inputField.text.Length != 0)
        {
            PlayerStats.Current = new PlayerStats(inputField.text);
            GunStats.Instance = new GunStats("Gun_0");
            SaveGame();
            Menu.Instance.StartGame();
        }
    }
    public void LoadGame()
    {
        string jString = File.ReadAllText(jsonName);
        if(jString.Length == 0)
        {
            return;
        }
        PlayerStats.Current = JsonUtility.FromJson<PlayerStats>(jString);
    }
    public void SaveGame()
    {
        string jString = JsonUtility.ToJson(new PlayerStats(inputField.text), true);
        File.WriteAllText(jsonName, jString);
    }
}
