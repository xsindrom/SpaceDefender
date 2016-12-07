using UnityEngine;
using System;
using System.Collections;

public class Timer
{
    private float timer;
    private float timeToComplete;
    public delegate void ActionForTimer();
    private event ActionForTimer actionToComplete;
    public float TimeToComplete
    {
        get { return timeToComplete; }
        set
        {
            if (value >= 0.0f)
            {
                timeToComplete = value;
            }
        }
    }
    public Timer()
    {
        timeToComplete = 0;
    }
    public Timer(float timeToComplete)
    {
        this.timeToComplete = timeToComplete;
    }
    public void AddAction(ActionForTimer action)
    {
        actionToComplete += action;
    }

    public void RemoveAction(ActionForTimer action)
    {
        actionToComplete -= action;
    }
    public void CompleteAction()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            if (actionToComplete != null)
            {
                actionToComplete();
            }
            timer = timeToComplete;
        }
    }
}
