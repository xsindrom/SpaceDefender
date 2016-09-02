﻿using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    private DeathHandler deathHandler;
    
    void Start()
    {
        deathHandler = new DeathHandler();
        deathHandler.AddAction(((IManager)ScoreManager.Instance).SendInfo);
        deathHandler.AddAction(((IManager)AchievementManager.Instance).SendInfo);
        deathHandler.AddAction(KillObject);
    }
    void KillObject()
    {
        Destroy(gameObject);
    }

    //--Test---
    void OnCollisionEnter2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject.tag == "Bullet")
        {
            ScoreManager.Instance.Score += 10.0f;
            deathHandler.OnDeath();
        }
    }
}