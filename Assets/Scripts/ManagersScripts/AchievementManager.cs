using UnityEngine;
using System.Collections;

public class AchievementManager : IManager
{
    private static AchievementManager instance;
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
    }
    void Awake()
    {
        instance = this;
    }
    //--Test---
    void IManager.SendInfo()
    {
        if (ScoreManager.Instance.Score == 100.0f)
        {
            Debug.Log("Well done, 100 points");
        }
    }
}
