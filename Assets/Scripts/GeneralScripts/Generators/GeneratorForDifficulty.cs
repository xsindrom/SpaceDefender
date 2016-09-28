using UnityEngine;
using System.Collections;

public class GeneratorForDifficulty : MonoBehaviour
{
    #region FIELDS
    [Header(StringHeadersInfo.TIMEPERIOD_Header)]
    public float periodOfTime = 0.0f;
    [Header(StringHeadersInfo.DELTA_Time_Header)]
    public float deltaDecreaseTime = 0.0f;
    [Header(StringHeadersInfo.TIME_Limits_Header)]
    public float minTime = 0.0f;
    private GeneratorForObjects generator;
    private Timer timerForActions;
    #endregion
    #region STANDART_EVENTS
    void Start()
    {
        generator = gameObject.GetComponent<GeneratorForObjects>();
        timerForActions = new Timer(periodOfTime);
        timerForActions.AddAction(IncreaseDifficulty);
    }
    void Update()
    {
        timerForActions.CompleteAction();
    }
    #endregion
    #region LOGIC
    private void IncreaseDifficulty()
    {
        if (generator.timeToGenerate > minTime)
        {
            generator.timeToGenerate -= deltaDecreaseTime;
        }
    }
    #endregion
}
