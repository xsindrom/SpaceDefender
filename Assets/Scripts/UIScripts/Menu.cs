using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System.Collections;

public class Menu : MonoBehaviour
{
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
        string path = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
        string jString = File.ReadAllText(path);
        JsonData jDataList = JsonMapper.ToObject(jString);
        if (jDataList == null)
        {
            jDataList = new JsonData();
            jDataList.SetJsonType(JsonType.Array);
        }
        if (!jDataList.IsArray) { jDataList.SetJsonType(JsonType.Array); }
        PlayerStats temp = new PlayerStats(jDataList.Count-1, path);
        if (temp == null)
        {
            temp = PlayerStats.Current;
            jDataList.Add(JsonMapper.ToObject(JsonUtility.ToJson(temp)));
            File.WriteAllText(path, jDataList.ToJson());
        }
        if (temp.Score < PlayerStats.Current.Score)
        {
            jDataList.Add(JsonMapper.ToObject(JsonUtility.ToJson(PlayerStats.Current)));
            File.WriteAllText(path, jDataList.ToJson());
        }
        #endregion
        OnSceneLeft(0);
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
