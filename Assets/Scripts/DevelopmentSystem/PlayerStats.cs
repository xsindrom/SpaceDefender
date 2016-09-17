using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections;
using System.Text;

[Serializable]
public class PlayerStats
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string name;
    [SerializeField]
    private int score;
    [SerializeField]
    private int level;

    public int Id
    {
        get { return id; }
        set
        {
            if (value >= 0) { id = value; }
        }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    [NonSerialized]
    private JsonData jData;

    public PlayerStats() { }
    public PlayerStats(string name)
    {
        this.Name = name;
        this.Id = 0;
        this.score = 0;
        this.level = 1;
    }
    public PlayerStats(int id, string jsonName)
    {
        LoadLeaderStats(id, jsonName);
    }
    public PlayerStats(int id, string name, int score, int level)
    {
        this.id = id;
        this.name = name;
        this.score = score;
        this.level = level;
    }
    public void LoadLeaderStats(int id, string jsonName)
    {
        string jString = File.ReadAllText(jsonName);
        jData = JsonMapper.ToObject(jString);
        this.Id = int.Parse(jData[id]["Id"].ToString());
        this.Name = jData[id]["Name"].ToString();
        this.score = int.Parse(jData[id]["Score"].ToString());
        this.level = int.Parse(jData[id]["Level"].ToString());
    }
    public void LoadPlayerStats(string jsonName)
    {
        string jString = File.ReadAllText(jsonName);
        this.Id = JsonUtility.FromJson<PlayerStats>(jString).id;
        this.Name = JsonUtility.FromJson<PlayerStats>(jString).name;
        this.Score = JsonUtility.FromJson<PlayerStats>(jString).score;
        this.Level = JsonUtility.FromJson<PlayerStats>(jString).level;
    }

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
}
