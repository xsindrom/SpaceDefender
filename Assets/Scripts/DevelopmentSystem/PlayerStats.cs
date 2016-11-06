using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections;
using System.Text;

[Serializable]
public class PlayerStats
{
    #region FIELDS
    [SerializeField]
    private int id;
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    [SerializeField]
    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            if (value != name)
            {
                name = value;
                GUIManager.Instance.NameToSet = name;    
            }
        }
    }
    public string NameLeader
    {
        get { return name; }
    }
    [SerializeField]
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            if (value != score)
            {
                score = value;
                GUIManager.Instance.ScoreToSet = score;
            }
        }
    }
    public int ScoreLeader
    {
        get { return score; }
    }
    [SerializeField]
    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            if (value >= 0 && value <= 30)
            {
                level = value;
                maxExperience = (int)Mathf.Pow(2, level);
                ScoreMultipler = level;
                GUIManager.Instance.LevelToSet = level;
            }
            if (Level > 30)
            {
                Level = 30;
                ScoreMultipler = level;
            }
            
        }
    }
    public int LevelLeader
    {
        get { return level; }
    }
    [SerializeField]
    private int experience = 0;
    public int Experience
    {
        get { return experience; }
        set
        {
            if (value != experience)
            {
                experience = value;
                if (experience >= maxExperience)
                {
                    Level++;
                }
            }
        }
    }
    [SerializeField]
    private int maxExperience;
    
    [SerializeField]
    private int scoreMultipler = 0;
    public int ScoreMultipler
    {
        get { return scoreMultipler; }
        set
        {
            if (value != scoreMultipler)
            {
                scoreMultipler = value;
                if (value < 0)
                {
                    scoreMultipler = 0;
                }
                GUIManager.Instance.ScoreMultiplerToSet = scoreMultipler;
            }
        }
    }
    #region Monetization
    [SerializeField]
    private float money = 0.0f;
    public float Money
    {
        get { return money; }
        set
        {
            if (value != money)
            {
                money = value;
                if (money < 0.0f)
                {
                    money = 0.0f;
                }
                GUIManager.Instance.MoneyToSet = money;
            }
        }
    }
    [SerializeField]
    private int returnItem = 0;
    public int ReturnItem
    {
        get { return returnItem; }
        set
        {
            if (value != returnItem)
            {
                returnItem = value;
                if (returnItem < 0)
                {
                    returnItem = 0;
                }
                GameManager.Instance.returnItem = returnItem;
                GUIManager.Instance.ReturnToLifeItemToSet = returnItem;
            }
        }
    }
    [SerializeField]
    private int destroyerForMeteorits = 0;
    public int DestroyerForMeteorits
    {
        get { return destroyerForMeteorits; }
        set
        {
            if (value != destroyerForMeteorits)
            {
                destroyerForMeteorits = value;
                if (destroyerForMeteorits < 0)
                {
                    destroyerForMeteorits = 0;
                }
                GameManager.Instance.destroyerForMeteorits = destroyerForMeteorits;
                GUIManager.Instance.DestroyerForMeteoritsItemToSet = destroyerForMeteorits;
            }
        }
    }
    [SerializeField]
    private int restoreAmmoItem = 0;
    public int RestoreAmmoItem
    {
        get { return restoreAmmoItem; }
        set
        {
            if (value != restoreAmmoItem)
            {
                restoreAmmoItem = value;
                if (restoreAmmoItem < 0)
                {
                    restoreAmmoItem = 0;
                }
                GameManager.Instance.restoreAmmoItem = restoreAmmoItem;
                GUIManager.Instance.RestoreAmmoItemToSet = restoreAmmoItem;
            }
        }
    }
    #endregion
    #endregion
    #region FILE_PROCESSING_PART
    [NonSerialized]
    private JsonData jData;
    public void LoadLeaderStats(int id, string jsonName)
    {
        #region GET_DATA
        string jString = File.ReadAllText(jsonName);
        if (jString.Length == 0) { return; }
        jData = JsonMapper.ToObject(jString);
        #endregion
        #region PARSE_DATA
        this.id = int.Parse(jData[id]["id"].ToString());
        this.name = jData[id]["name"].ToString();
        this.score = int.Parse(jData[id]["score"].ToString());
        this.level = int.Parse(jData[id]["level"].ToString());
        #endregion
    }
    public void LoadPlayerStats(string jsonName)
    {
        #region GET_DATA
        string jString = File.ReadAllText(jsonName);
        #endregion
        #region PARSE_DATA
        PlayerStats tempStat = JsonUtility.FromJson<PlayerStats>(jString);
        if (tempStat == null)
        {
            return;
        }
        this.Id = tempStat.id;
        this.Name = tempStat.name;
        this.Score = tempStat.score;
        this.Level = tempStat.level;
        this.Experience = tempStat.experience;
        this.ScoreMultipler = tempStat.scoreMultipler;
        this.Money = tempStat.money;
        this.ReturnItem = tempStat.returnItem;
        this.DestroyerForMeteorits = tempStat.destroyerForMeteorits;
        this.ReturnItem = tempStat.returnItem;
        this.RestoreAmmoItem = tempStat.restoreAmmoItem;
        #endregion
    }
    public void SavePlayerStats(string jsonName)
    {
        File.WriteAllText(jsonName, JsonUtility.ToJson(Current));
    }
    #endregion
    #region CONSTRUCTORS
    public PlayerStats()
    {
        this.Name = "not_set";
        this.Id = 0;
        this.Score = 0;
        this.Level = 0;
        this.Experience = 0;
        this.ScoreMultipler = 1;
        this.Money = 0;
        this.DestroyerForMeteorits = 0;
        this.ReturnItem = 0;
        this.RestoreAmmoItem = 0;
    }
    public PlayerStats(string name)
    {
        this.Name = name;
        this.Id = 0;
        this.Score = 0;
        this.Level = 1;
        this.Experience = 0;
        this.ScoreMultipler = 1;
        this.Money = 0;
        this.ReturnItem = 0;
        this.DestroyerForMeteorits = 0;
        this.DestroyerForMeteorits = 0;
        this.ReturnItem = 0;
        this.RestoreAmmoItem = 0;
    }
    public PlayerStats(int id, string jsonName)
    {
        LoadLeaderStats(id, jsonName);
    }
    public PlayerStats(int id, string name, int score, int level, int scoreMultipler)
    {
        this.Id = id;
        this.Name = name;
        this.Score = score;
        this.Level = level;
        this.Experience = 0;
        this.ScoreMultipler = scoreMultipler;
        this.Money = 0;
        this.ReturnItem = 0;
        this.DestroyerForMeteorits = 0;
        this.RestoreAmmoItem = 0;
    }
    public PlayerStats(PlayerStats playerStats)
    {
        if (playerStats == null)
        {
            return;
        }
        this.Id = playerStats.Id;
        this.Name = playerStats.Name;
        this.Score = playerStats.Score;
        this.Level = playerStats.Level;
        this.Experience = playerStats.Experience;
        this.ScoreMultipler = playerStats.ScoreMultipler;
        this.Money = playerStats.Money;
        this.ReturnItem = playerStats.ReturnItem;
        this.DestroyerForMeteorits = playerStats.DestroyerForMeteorits;
        this.ReturnItem = playerStats.ReturnItem;
        this.RestoreAmmoItem = playerStats.RestoreAmmoItem;
    }
    public readonly static PlayerStats Empty = new PlayerStats("empty");
    public override bool Equals(object obj)
    {
        if (obj == null) { return false; }
        PlayerStats toCompare = (PlayerStats)obj;
        if (toCompare == null) { return false; }
        if (this.id == toCompare.id)
            if (this.score == toCompare.score)
                if (this.level == toCompare.level)
                    if (this.experience == toCompare.experience)
                        if (this.scoreMultipler == toCompare.scoreMultipler)
                            if (this.money == toCompare.money)
                                if (this.name.Equals(toCompare.name))
                                    return true;
        return false;
    }
    public override int GetHashCode()
    {
        int toReturn = 0;
        foreach (char c in name)
        {
            toReturn += c;
        }
        toReturn += score;
        toReturn += level;
        toReturn += experience;
        toReturn += scoreMultipler;
        toReturn += (int)money;
        toReturn += returnItem;
        toReturn += destroyerForMeteorits;
        return toReturn;
    }
    #endregion

    #region SINGLETON
    [NonSerialized]
    private static PlayerStats current = PlayerStats.Empty;
    public static PlayerStats Current
    {
        get { return current; }
        set
        {
            if (value != null)
            {
                current = value;
            }
        }
    }
    #endregion

    #region LOGIC
    public void IncreaseLevel(int experienceToAdd)
    {
        Experience += experienceToAdd;
    }
    #endregion
}
