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
    [SerializeField]
    private int scoreRank;
    public int Score
    {
        get { return score; }
        set
        {
            if (value != score)
            {
                score = value;
                GUIManager.Instance.ScoreToSet = score;
                float valueForAchievement = (float)AchievementManager.AchievementType.SCORE_achievement * scoreRank * 1.5f;
                AchievementManager.Instance.CreateAchievement(score, AchievementManager.AchievementType.SCORE_achievement,
                                                              ref scoreRank, StringCaptionsInfo.SCORE_achievementText,
                                                              valueForAchievement);
            }
        }
    }
    public int ScoreLeader
    {
        get { return score; }
    }
    [SerializeField]
    private int level;
    [SerializeField]
    private int levelRank;
    private int CalculateLevelLessEleven(int level)
    {
        return (int)(40.0f * Mathf.Pow(level, 2) + 360.0f * level);
    }
    private int CalculateLevelUpperEleven(int level)
    {
        return (int)(-0.4f * Math.Pow(level, 3) + 40.4f * Math.Pow(level, 2) + 396.0f * level);
    }
    public int Level
    {
        get { return level; }
        set
        {
            if (value != level)
            {
                if (value >= 0 && value <= 30)
                {
                    level = value;
                    experience = 0;
                    maxExperience = (level > 0 && level < 11) ? CalculateLevelLessEleven(level) : CalculateLevelUpperEleven(level);
                    ScoreMultipler = level;
                    GUIManager.Instance.LevelToSet = level;
                    AchievementManager.Instance.CreateAchievement(level, AchievementManager.AchievementType.LEVEL_achievement,
                                                                  ref levelRank, StringCaptionsInfo.LEVEL_achievementText,
                                                                  (float)AchievementManager.AchievementType.LEVEL_achievement * levelRank);
                }
                if (level > 30)
                {
                    level = 30;
                    ScoreMultipler = level;
                }
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
                GUIManager.Instance.ScoreMultiplerToSet = scoreMultipler;
            }
        }
    }
    #region Monetization
    [SerializeField]
    private float money = 0.0f;
    [SerializeField]
    private int moneyRank;
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
                AchievementManager.Instance.CreateAchievement(money, AchievementManager.AchievementType.MONEY_achievement,
                                                              ref moneyRank, StringCaptionsInfo.MONEY_achievementText,
                                                              (float)AchievementManager.AchievementType.MONEY_achievement * moneyRank);
            }
        }
    }
    [SerializeField]
    private int destroyerForMeteorits = 0;
    [SerializeField]
    private int destroyerRank;
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
                AchievementManager.Instance.CreateAchievement(destroyerForMeteorits, AchievementManager.AchievementType.SHOP_achievement,
                                                              ref destroyerRank, StringCaptionsInfo.SHOP_destroyMeteorsAchievementText,
                                                              (float)AchievementManager.AchievementType.SHOP_achievement * destroyerRank);
            }
        }
    }
    [SerializeField]
    private int returnItem = 0;
    [SerializeField]
    private int returnRank;
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
                AchievementManager.Instance.CreateAchievement(returnItem, AchievementManager.AchievementType.SHOP_achievement,
                                                              ref returnRank, StringCaptionsInfo.SHOP_returnToLifeAchievementText,
                                                              (float)AchievementManager.AchievementType.SHOP_achievement * returnRank);
            }
        }
    }
    [SerializeField]
    private int restoreAmmoItem = 0;
    [SerializeField]
    private int restoreRank;
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
                AchievementManager.Instance.CreateAchievement(restoreAmmoItem, AchievementManager.AchievementType.SHOP_achievement,
                                                              ref restoreRank, StringCaptionsInfo.SHOP_restoreAmmoAchievementText,
                                                              (float)AchievementManager.AchievementType.SHOP_achievement * restoreRank);
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
        string jString = File.ReadAllText(jsonName);
        if (jString.Length == 0) { return; }
        jData = JsonMapper.ToObject(jString);
        this.id = int.Parse(jData[id]["id"].ToString());
        this.name = jData[id]["name"].ToString();
        this.score = int.Parse(jData[id]["score"].ToString());
        this.level = int.Parse(jData[id]["level"].ToString());
    }
    public void LoadPlayerStats(string jsonName)
    {
        string jString = File.ReadAllText(jsonName);
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
        this.scoreRank = tempStat.scoreRank;
        this.levelRank = tempStat.levelRank;
        this.moneyRank = tempStat.moneyRank;
        this.destroyerRank = tempStat.destroyerRank;
        this.returnRank = tempStat.returnRank;
        this.restoreRank = tempStat.restoreRank;
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
        this.restoreRank = 1;
        this.returnRank = 1;
        this.destroyerRank = 1;
        this.levelRank = 2;
        this.scoreRank = 1;
        this.moneyRank = 1;
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
        this.restoreRank = 1;
        this.returnRank = 1;
        this.destroyerRank = 1;
        this.levelRank = 2;
        this.scoreRank = 1;
        this.moneyRank = 1;
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
        this.restoreRank = 1;
        this.returnRank = 1;
        this.destroyerRank = 1;
        this.levelRank = 2;
        this.scoreRank = 1;
        this.moneyRank = 1;
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
        this.restoreRank = playerStats.restoreRank;
        this.returnRank = playerStats.returnRank;
        this.destroyerRank = playerStats.destroyerRank;
        this.levelRank = playerStats.levelRank;
        this.scoreRank = playerStats.scoreRank;
        this.moneyRank = playerStats.moneyRank;
    }
    public readonly static PlayerStats Empty = new PlayerStats("empty");
    #endregion

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

    #region LOGIC
    public void IncreaseLevel(int baseExperience)
    {
        Experience += (baseExperience * 5) + 45;
    }
    #endregion
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
}
