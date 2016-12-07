using UnityEngine;
using System.Collections;

public class GeneratorForDifficulty : MonoBehaviour
{
    [Header(StringHeadersInfo.TIMEPERIOD_Header)]
    public float periodOfTime = 0.0f;
    [Header(StringHeadersInfo.DELTA_Time_Header)]
    public float deltaDecreaseTime = 0.0f;
    [Header(StringHeadersInfo.TIME_Limits_Header)]
    public float minTime = 0.0f;
    private GeneratorForObjects generator;
    private Timer timerForActions;
    private int playerLevel;
    void Awake()
    {
        playerLevel = PlayerStats.Current.Level;
    }
    void Start()
    {
        generator = gameObject.GetComponent<GeneratorForObjects>();
        generator.minAmount = 1;
        generator.maxAmount = playerLevel;
        timerForActions = new Timer(periodOfTime);
        timerForActions.AddAction(IncreaseDifficultyTime);
        timerForActions.AddAction(IncreaseDifficultyAmount);
    }
    void Update()
    {
        timerForActions.CompleteAction();
        if (playerLevel < PlayerStats.Current.Level)
        {
            IncreaseDifficultyAmount();
            playerLevel = PlayerStats.Current.Level;
        }
        else
        {
            return;
        }
    }
    private void IncreaseDifficultyTime()
    {
        if (generator.timeToGenerate > minTime)
        {
            generator.timeToGenerate -= deltaDecreaseTime;
        }
    }
    private void IncreaseDifficultyAmount()
    {
        generator.minAmount++;
        generator.maxAmount++;
    }
}
