using UnityEngine;
using System;
using System.Collections;

public class Timer
{
    #region FIELDS
    private float timer;
    private float timeToComplete;
    public delegate void ActionForTimer();
    private event ActionForTimer actionToComplete;
    #endregion
    #region PROPERTIES
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
    #endregion
    #region CONSTRUCTORS
    public Timer()
    {
        timeToComplete = 0;
    }
    public Timer(float timeToComplete)
    {
        this.timeToComplete = timeToComplete;
    }
    #endregion
    #region ADDING_ACTIONS
    public void AddAction(ActionForTimer action)
    {
        actionToComplete += action;
    }

    public void RemoveAction(ActionForTimer action)
    {
        actionToComplete -= action;
    }
    #endregion
    #region PERFORM_ACTIONS
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
    #endregion
}
