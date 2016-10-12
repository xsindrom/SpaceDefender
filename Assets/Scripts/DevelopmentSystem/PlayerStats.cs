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
            if (value != null)
            {
                name = value;
                GUIManager.Instance.NameToSet = name;
            }
        }
    }
    [SerializeField]
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    [SerializeField]
    private int level;
    [SerializeField]
    private int experience = 0;
    public int Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            if (experience >= maxExperience)
            {
                Level++;
            }
        }
    }
    [SerializeField]
    private int maxExperience;
    public int Level
    {
        get { return level; }
        set
        {
            if (value > 0)
            {
                level = value;
                maxExperience = 100 * (int)Mathf.Pow(2, level);
                scoreMultipler++;
                GUIManager.Instance.LevelToSet = level;
            }
        }
    }
    [SerializeField]
    private int scoreMultipler = 0;
    public int ScoreMultipler
    {
        get { return scoreMultipler; }
        set
        {
            if (value > 0)
            {
                scoreMultipler = value;
                GUIManager.Instance.ScoreMultiplerToSet = scoreMultipler;
            }
        }
    }
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
        this.Experience = int.Parse(jData[id]["experience"].ToString());
        this.Level = int.Parse(jData[id]["level"].ToString());
        this.scoreMultipler = int.Parse(jData[id]["scoreMultipler"].ToString());
        #endregion
    }
    public void LoadPlayerStats(string jsonName)
    {
        #region GET_DATA
        string jString = File.ReadAllText(jsonName);
        #endregion
        #region PARSE_DATA
        this.id = JsonUtility.FromJson<PlayerStats>(jString).id;
        this.name = JsonUtility.FromJson<PlayerStats>(jString).name;
        this.score = JsonUtility.FromJson<PlayerStats>(jString).score;
        this.Level = JsonUtility.FromJson<PlayerStats>(jString).level;
        this.Experience = JsonUtility.FromJson<PlayerStats>(jString).experience;
        this.scoreMultipler = JsonUtility.FromJson<PlayerStats>(jString).scoreMultipler;
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
        this.name = "not_set";
        this.id = 0;
        this.score = 0;
        this.Level = 0;
        this.Experience = 0;
        this.scoreMultipler = 1;
    }
    public PlayerStats(string name)
    {
        this.name = name;
        this.id = 0;
        this.score = 0;
        this.Level = 1;
        this.Experience = 0;
        this.scoreMultipler = 1;
    }
    public PlayerStats(int id, string jsonName)
    {
        LoadLeaderStats(id, jsonName);
    }
    public PlayerStats(int id, string name, int score, int level, int scoreMultipler)
    {
        this.id = id;
        this.name = name;
        this.score = score;
        this.Level = level;
        this.Experience = 0;
        this.scoreMultipler = scoreMultipler;
    }
    public readonly static PlayerStats Empty = new PlayerStats();
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
        return toReturn;
    }
    #endregion

    #region SINGLETON
    [NonSerialized]
    private static PlayerStats current;
    public static PlayerStats Current
    {
        get
        {
            if (current == null)
            {
                current = new PlayerStats();
            }
            return current;
        }
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
