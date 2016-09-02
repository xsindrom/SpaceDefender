using UnityEngine;
using System.Collections;

public class GeneratorForDifficulty : MonoBehaviour
{
    public float periodOfTime = 0.0f;
    public float deltaDecreaseTime = 0.0f;
    public float minTime = 0.0f;

    private GeneratorForObjects generator;
    private Timer timerForActions;
    void Start()
    {
        generator = gameObject.GetComponent<GeneratorForObjects>();
        timerForActions = new Timer(periodOfTime);
        timerForActions.AddAction(IncreaseDifficulty);
    }
    private void IncreaseDifficulty()
    {
        if (generator.timeToGenerate > minTime)
        {
            generator.timeToGenerate -= deltaDecreaseTime;
        }
    }
    void Update()
    {
        timerForActions.CompleteAction();
    }
}
