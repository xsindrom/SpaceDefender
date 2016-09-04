using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    private DeathHandler deathHandler;
    
    void Start()
    {
        deathHandler = new DeathHandler();
    }

    //--Test---
    void OnCollisionEnter2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject.tag == "Bullet")
        {
            ScoreManager.Instance.Score += 10.0f;
            deathHandler.OnDeath();
            Destroy(gameObject);
        }
    }
}
