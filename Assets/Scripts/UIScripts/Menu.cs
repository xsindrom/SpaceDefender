using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Menu : MonoBehaviour
{
    public static string pathLeaders = "";
    public static string pathCurrent = "";
    public static string pathAchieves = "";
    public static string pathConfig = "";
    public static List<Scene> scenes = new List<Scene>();
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
    
    void Awake()
    {
        scenes.Add(SceneManager.GetActiveScene());
        if (SceneManager.GetActiveScene().buildIndex == 0 && scenes.Count == 1)
        {
            pathLeaders = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
            pathCurrent = Application.persistentDataPath + StringPathsInfo.CURRENT_PLAYERSTATS_PATH;
            pathAchieves = Application.persistentDataPath + StringPathsInfo.ACHIEVEMENTS_PATH;
            pathConfig = Application.persistentDataPath + StringPathsInfo.CONFIG_PATH;

            pathLeaders.CreateFileAsDirectedByPath();
            pathCurrent.CreateFileAsDirectedByPath();
            pathAchieves.CreateFileAsDirectedByPath();
            pathConfig.CreateFileAsDirectedByPath();

            GunRendererScript.baseSprite = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_base_IMAGES_PATH);
            GunRendererScript.barrelSprite = Resources.LoadAll<Sprite>(StringPathsInfo.GUNS_barrel_IMAGES_PATH);
        }
    }
    void Start()
    {
        LoadSettings();
        LoadGame();
        if (AchievementManager.Instance.AllAchievements.Count == 0)
        {
            AchievementManager.Instance.LoadAchievements();
        }
    }
    #region LOGIC
    public void LoadGame()
    {
        string jString = File.ReadAllText(pathCurrent);
        if (jString.Length == 0) { return; }
        PlayerStats.Current = new PlayerStats(JsonUtility.FromJson<PlayerStats>(jString));
    }
    public void GoToMainMenu()
    {
        SaveLeader();
        OnSceneLeft(0);
    }

    private void WorkWithListPlayer(JsonData jDataList)
    {
        List<PlayerStats> listPlayer = new List<PlayerStats>();
        for (int index = 0, size = jDataList.Count; index < size; index++)
        {
            listPlayer.Add(new PlayerStats(index, pathLeaders));
        }
        if (listPlayer.Count == 0) { listPlayer.Add(PlayerStats.Current); }
        for (int index = 0, size = listPlayer.Count; index < size; index++)
        {
            if (PlayerStats.Current.Score > listPlayer[index].Score)
            {
                listPlayer.Insert(index, PlayerStats.Current);
                break;
            }
        }
        for (int index = 0, size = listPlayer.Count; index < size; index++)
        {
            listPlayer[index].Id = index + 1;
        }
        if (listPlayer.Count > 10)
        {
            listPlayer.RemoveRange(10, listPlayer.Count - 10);
        }
        jDataList = new JsonData();
        jDataList.SetJsonType(JsonType.Array);
        foreach (PlayerStats playerInList in listPlayer)
        {
            jDataList.Add(JsonMapper.ToObject(JsonUtility.ToJson(playerInList)));
        }
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
    public void SaveAchievements()
    {
        AchievementManager.Instance.SaveAchievements();
    }
    public void LoadAchievements()
    {
        AchievementManager.Instance.LoadAchievements();
    }
    public void SaveSettings()
    {
        SettingsScript.VolumeSettings volumeSettings = new SettingsScript.VolumeSettings(SettingsScript.EffectVolume,
                                                                                         SettingsScript.MusicVolume);
        File.WriteAllText(pathConfig, JsonUtility.ToJson(volumeSettings, true));
    }
    public void LoadSettings()
    {
        string jString = File.ReadAllText(pathConfig);
        if (jString.Length == 0)
        {
            return;
        }
        SettingsScript.VolumeSettings volumeSettings = JsonUtility.FromJson<SettingsScript.VolumeSettings>(jString);
        if (volumeSettings == null)
        {
            return;
        }
        SettingsScript.EffectVolume = volumeSettings.effectVolume;
        SettingsScript.MusicVolume = volumeSettings.musicVolume;

        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.MUSIC_tag);
        GameObject[] effectObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.EFFECT_tag);
        if (musicObjects != null)
        {
            foreach (GameObject musicObject in musicObjects)
            {
                musicObject.GetComponent<AudioSource>().volume = SettingsScript.MusicVolume;
            }
        }
        if (effectObjects != null)
        {
            foreach (GameObject effectOjbect in effectObjects)
            {
                effectOjbect.GetComponent<AudioSource>().volume = SettingsScript.EffectVolume;
            }
        }
    }
    #endregion
}
