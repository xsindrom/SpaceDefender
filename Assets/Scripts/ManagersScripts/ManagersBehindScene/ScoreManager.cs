using UnityEngine;
using System.Collections;

public class ScoreManager : IManager
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ScoreManager();
            }
            return instance;
        }
    }

    private float score;
    public float Score
    {
        get { return score; }
        set
        {
            if (value != score)
            {
                score = value;
                ((IManager)this).SendInfo();
            }
        }
    }

    void Awake()
    {
        instance = this;
    }
    //--Test---
    void IManager.SendInfo()
    {
        Debug.Log(StringCaptionsInfo.SCORE_Caption + Score);
    }
    string IManager.GetInfo()
    {
        return StringCaptionsInfo.SCORE_Caption + Score;
    }
}
