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
#if UNITY_EDITOR
            Debug.Log("Well done, 100 points");
#endif
        }
    }
    string IManager.GetInfo()
    {
        return "Well done, 100 points";
    }
}
