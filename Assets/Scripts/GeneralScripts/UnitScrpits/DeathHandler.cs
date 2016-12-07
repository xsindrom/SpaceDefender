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
    public void AddAction(DeathEvent action)
    {
        deathDetected += action;
    }
    public void RemoveAction(DeathEvent action)
    {
        deathDetected -= action;
    }
    public void OnDeath()
    {
        if (deathDetected != null)
        {
            deathDetected();
        }
    }
}
