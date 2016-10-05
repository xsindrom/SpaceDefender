using UnityEngine;
using System.Collections;

public class ScoreManager : IManager
{
    #region SINGLETON
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
    #endregion
    #region FIELDS
    private int score;
    #endregion
    #region PROPERTIES
    public int Score
    {
        get { return score; }
        set
        {
            if (value != score)
            {
                score = value;
                //((IManager)this).SendInfo();
                GUIManager.Instance.ScoreToSet = score;
                PlayerStats.Current.Score = score;
            }
        }
    }
    #endregion
    #region CONSTRUCTORS
    private ScoreManager() { }
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        instance = this;
    }
    #endregion
    #region LOGIC
    public void AddScore(int score, int scoreMultipler)
    {
        Score += score * scoreMultipler;
    }
    void IManager.SendInfo()
    {
#if UNITY_EDITOR
        Debug.Log(StringCaptionsInfo.SCORE_Caption + Score);
#endif
    }

    string IManager.GetInfo()
    {
        return Score.ToString();
    }
    #endregion
}
