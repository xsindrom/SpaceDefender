using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Menu : MonoBehaviour
{
    #region Fields
    public string pathLeaders = "";
    public string pathCurrent = "";
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
        SceneManager.sceneLoaded += delegate { (Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName).CreateFileAsDirectedByPath(); };
        SceneManager.sceneLoaded += delegate { (Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH).CreateFileAsDirectedByPath(); };
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= delegate { (Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName).CreateFileAsDirectedByPath(); };
        SceneManager.sceneLoaded -= delegate { (Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH).CreateFileAsDirectedByPath(); };
    }
    void Awake()
    {
        pathLeaders = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
        pathCurrent = Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH;
    }
    void Start()
    {
        LoadGame();
    }
    #endregion
    #region LOGIC
    public void LoadGame()
    {
        string jString = File.ReadAllText(pathCurrent);
        if (jString.Length == 0) { return; }
        PlayerStats.Current = new PlayerStats(JsonUtility.FromJson<PlayerStats>(jString));
    }
    public void GoToMainMenu()
    {
        #region FILE_PROCESSING PART
        SaveLeader();
        #endregion
        OnSceneLeft(0);
    }
    
    private void WorkWithListPlayer(JsonData jDataList)
    {
        List<PlayerStats> listPlayer = new List<PlayerStats>();
        #region InitList
        for (int index = 0; index < jDataList.Count; index++)
        {
            listPlayer.Add(new PlayerStats(index, pathLeaders));
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
        if (listPlayer.Count > 10)
        {
            listPlayer.RemoveRange(10, listPlayer.Count-10);
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
        File.WriteAllText(pathLeaders, jDataList.ToJson());
    }
    public void StartGame()
    {
        SetValuesToNull();
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        PlayerStats.Current.SavePlayerStats(pathCurrent);
        Application.Quit();
    }
    public void RestartScene()
    {
        SaveLeader();
        OnSceneLeft(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnSceneLeft(int index)
    {
        SetValuesToNull();
        PlayerStats.Current.SavePlayerStats(pathCurrent);
        SceneManager.LoadScene(index);
    }
    public void SetValuesToNull()
    {
        PlayerStats.Current.Score = 0;
        GunStats.Instance.AmmoStats.CurrentAmmo = GunStats.Instance.AmmoStats.AmmoSize;
        GameManager.Instance.Health = 100;
    }
    public void SaveLeader()
    {
        string jString = File.ReadAllText(pathLeaders);
        JsonData jDataList = JsonMapper.ToObject(jString);
        if (jDataList == null)
        {
            jDataList = new JsonData();
            jDataList.SetJsonType(JsonType.Array);
        }
        if (!jDataList.IsArray) { jDataList.SetJsonType(JsonType.Array); }
        WorkWithListPlayer(jDataList);
    }
    #endregion
}
