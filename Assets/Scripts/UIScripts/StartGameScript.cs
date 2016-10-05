﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using System;
using System.IO;
using System.Collections;

public class StartGameScript : MonoBehaviour
{
    public InputField inputField;
    #region FILE_PROCESSING_PART
    private JsonData jData;
    private string jsonName;
    void CreateJsonFile(string jsonName)
    {
        if (!File.Exists(jsonName))
        {
            File.Create(jsonName);
        }
    }
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        jsonName = Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH;
        if (jData == null)
        {
            jData = new JsonData();
        }
        CreateJsonFile(jsonName);
    }
    #endregion
    #region LOGIC
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
    public void LoadPreviousGame()
    {
        if(!GunStats.Instance.Equals(GunStats.Empty))
        {
            LoadGame();
            if (!PlayerStats.Current.Equals(PlayerStats.Empty))
            {
                Menu.Instance.StartGame();
            }
        }
    }
    public void LoadGame()
    {
        string jString = File.ReadAllText(jsonName);
        if (jString.Length == 0) { return; }
        PlayerStats.Current = JsonUtility.FromJson<PlayerStats>(jString);
    }
    public void SaveGame()
    {
        string jString = JsonUtility.ToJson(PlayerStats.Current, true);
        File.WriteAllText(jsonName, jString);
    }
    #endregion
}
