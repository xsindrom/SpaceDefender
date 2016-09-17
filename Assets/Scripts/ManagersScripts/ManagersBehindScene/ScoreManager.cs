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
    private ScoreManager() { }

    public void AddScore(float score,float scoreMultipler)
    {
        Score += score * scoreMultipler;
    }
    
    void Awake()
    {
        instance = this;
    }
    
   
    void IManager.SendInfo()
    {
        Debug.Log(StringCaptionsInfo.SCORE_Caption + Score);
    }
    string IManager.GetInfo()
    {
        return StringCaptionsInfo.SCORE_Caption + Score;
    }
}
