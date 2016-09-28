using UnityEngine;
using System;
using System.Collections;
public class DeathHandler
{
    public delegate void DeathEvent();
    private event DeathEvent deathDetected;

    public void Death()
    {
        OnDeath();
    }
    #region ADD_ACTIONS
    public void AddAction(DeathEvent action)
    {
        deathDetected += action;
    }
    public void RemoveAction(DeathEvent action)
    {
        deathDetected -= action;
    }
    #endregion
    #region PERFOM_ACTIONS
    public void OnDeath()
    {
        if (deathDetected != null)
        {
            deathDetected();
        }
    }
    #endregion
}
