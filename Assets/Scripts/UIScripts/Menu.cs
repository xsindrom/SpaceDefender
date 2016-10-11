using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Menu : MonoBehaviour
{
    #region Fields
    public string path = "";
    #endregion
    #region SINGLETON
    private static Menu instance;
    public static Menu Instance
    {
        get
        {
            instance = Object.FindObjectOfType<Menu>();
            if (instance == null)
            {
                GameObject menuObject = new GameObject(StringNamesInfo.MENU_name);
                menuObject.AddComponent<Menu>();
                instance = menuObject.gameObject.GetComponent<Menu>();
            }
            return instance;
        }
        set
        {
            if (value != null)
            {
                instance = value;
            }
        }
    }
    #endregion
    #region STANDART_EVENTS
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += delegate { CreateJsonFiles(Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName); };
        SceneManager.sceneLoaded += delegate { CreateJsonFiles(Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH); };
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= delegate { CreateJsonFiles(Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName); };
        SceneManager.sceneLoaded -= delegate { CreateJsonFiles(Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH); };
    }
    void Awake()
    {
        path = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
    }
    #endregion
    #region LOGIC
    void CreateJsonFiles(string jsonName)
    {
        if (!File.Exists(jsonName))
        {
            File.Create(jsonName);
        }
    }
    public void GoToMainMenu()
    {
        #region FILE_PROCESSING PART
        string jString = File.ReadAllText(path);
        JsonData jDataList = JsonMapper.ToObject(jString);
        if (jDataList == null)
        {
            jDataList = new JsonData();
            jDataList.SetJsonType(JsonType.Array);
        }
        if (!jDataList.IsArray) { jDataList.SetJsonType(JsonType.Array); }
        WorkWithListPlayer(jDataList);
        #endregion
        OnSceneLeft(0);
    }
    
    private void WorkWithListPlayer(JsonData jDataList)
    {
        List<PlayerStats> listPlayer = new List<PlayerStats>();
        #region InitList
        for (int index = 0; index < jDataList.Count; index++)
        {
            listPlayer.Add(new PlayerStats(index, path));
        }
        #endregion
        if (listPlayer.Count == 0) { listPlayer.Add(PlayerStats.Current); }
        #region ProcessList
        for (int index = 0; index < listPlayer.Count; index++)
        {
            if (PlayerStats.Current.Score > listPlayer[index].Score)
            {
                listPlayer.Insert(index, PlayerStats.Current);
                break;
            }
        }
        #endregion
        jDataList = new JsonData();
        jDataList.SetJsonType(JsonType.Array);
        #region TransfromListToJdataList
        foreach (PlayerStats playerInList in listPlayer)
        {
            jDataList.Add(JsonMapper.ToObject(JsonUtility.ToJson(playerInList)));
        }
        #endregion
        File.WriteAllText(path, jDataList.ToJson());
    }
    public void StartGame()
    {
        SetValuesToNull();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        PlayerStats.Current.SavePlayerStats(Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH);
        Application.Quit();
    }
    public void RestartScene()
    {
        OnSceneLeft(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSceneLeft(int index)
    {
        PlayerStats.Current.SavePlayerStats(Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH);
        SetValuesToNull();
        SceneManager.LoadScene(index);
    }
    public void SetValuesToNull()
    {
        ScoreManager.Instance.Score = 0;
        GunStats.Instance.AmmoStats.CurrentAmmo = GunStats.Instance.AmmoStats.AmmoSize;
        GameManager.Instance.Health = 100;
    }
    #endregion
}
