using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System;
using System.IO;
using System.Collections.Generic;
[Serializable]
public class AchievementManager
{
    public enum AchievementType: int
    {
        LEVEL_achievement = 1,
        MONEY_achievement = 100,
        SCORE_achievement = 1000,
        SHOP_achievement = 1
    }
    public enum OperationType
    {
        multiplicity,
        power
    }
    [Serializable]
    
    public class Achievement
    {
        [SerializeField]
        public AchievementType achievementType;
        [SerializeField]
        public int rank = 1;
        [SerializeField]
        public float baseValue;
        [SerializeField]
        public string achievementText;
        public Achievement()
        {
            achievementType = AchievementType.LEVEL_achievement;
            rank = 1;
            baseValue = (float)achievementType;
            achievementText = "";
        }
        public Achievement(AchievementType achievementType, int rank, string achievementText)
        {
            this.achievementType = achievementType;
            this.rank = rank;
            this.baseValue = (float)achievementType;
            this.achievementText = achievementText;
        }
        public Achievement GetAchievement(int rank, AchievementType achievementType)
        {
            if (rank == this.rank && achievementType == this.achievementType)
            {
                return new Achievement(this.achievementType, this.rank, this.achievementText);
            }
            return new Achievement();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Achievement toCompare = (Achievement)obj;
            if (toCompare == null) return false;
            if (toCompare.achievementType == this.achievementType)
            {
                if (toCompare.rank == this.rank)
                {
                    if (toCompare.baseValue == this.baseValue)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    private static AudioSource achievementSound;
    private static AchievementManager instance = null;
    public static AchievementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AchievementManager();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private List<Achievement> allAchievements = new List<Achievement>();
    public List<Achievement> AllAchievements
    {
        get { return allAchievements; }
    }

    public void AddAchievement(Achievement achievement)
    {
        if (!allAchievements.Contains(achievement))
        {
            allAchievements.Add(achievement);
        }
    }
    public void DisplayAchievement(string achievement)
    {
        GameObject achievementsObject = GameObject.Find("Achievements");
        ToastAchievementFillerScript content = achievementsObject.GetComponent<ToastAchievementFillerScript>();
        GameObject prefabValue = GameObject.Instantiate(content.prefabToInstantiate) as GameObject;
        prefabValue.GetComponentInChildren<Text>().text = achievement;
        content.AddFillerObject(prefabValue);
        MyExtensionMethods.InitAudio(ref achievementSound, StringNamesInfo.ACHIEVEMENT_SOUND);
        if(achievementSound && SettingsScript.EffectVolume > 0.01f) achievementSound.Play();
    }
    
    public void CreateAchievement(float value, AchievementType achievementType, ref int rank, string achievementText, float achievementValue)
    {
        Achievement toAdd = new Achievement(achievementType, rank, achievementText);
        float valueToCompare = achievementValue;
        if (value >= valueToCompare)
        {
            string achievementFullText = string.Format(achievementText, valueToCompare);
            toAdd.achievementText = achievementFullText;
            if (rank != 0)
            {
                DisplayAchievement(achievementFullText);
                AddAchievement(toAdd);
                if (rank > 1)
                {
                    for (int index = rank-1; index > 0; index--)
                    {
                        allAchievements.Remove(new Achievement(achievementType, index, ""));
                    }
                }
                rank++;
            }
        }
    }
    public void SaveAchievements()
    {
        string path = Application.persistentDataPath + StringPathsInfo.ACHIEVEMENTS_PATH;
        path.CreateFileAsDirectedByPath();
        JsonData jsonArray = new JsonData();
        jsonArray.SetJsonType(JsonType.Array);
        foreach (Achievement achieve in allAchievements)
        {
            jsonArray.Add(JsonMapper.ToObject(JsonUtility.ToJson(achieve)));
        }
        File.WriteAllText(path, JsonMapper.ToJson(jsonArray));
    }
    public void LoadAchievements()
    {
        string path = Application.persistentDataPath + StringPathsInfo.ACHIEVEMENTS_PATH;
        string jString = File.ReadAllText(path);
        if (jString.Length == 0)
        {
            return;
        }
        allAchievements = new List<Achievement>();
        JsonData jsonArray = JsonMapper.ToObject(jString);
        if (jsonArray.Count == 0)
        {
            return;
        }
        for (int index = 0; index < jsonArray.Count; index++)
        {
            allAchievements.Add(JsonUtility.FromJson<Achievement>(jsonArray[index].ToJson()));
        }
    }

    
}
