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
        set { score = value; }
    }

    void Awake()
    {
        instance = this;
    }

    void IManager.PerfomManager()
    {
        Debug.Log("Ur score is: " + score);
    }
}
