using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    public float lifeCost;
    private DeathHandler deathHandler;
    
    void Start()
    {
        deathHandler = new DeathHandler();
        deathHandler.AddAction(AddScore);
    }
    void AddScore()
    {
        ScoreManager.Instance.AddScore(lifeCost, (float)GunStats.scoreMultipler);
    }
    void OnCollisionEnter2D(Collision2D collisionToDetect)
    {
        if (collisionToDetect.gameObject.tag == StringNamesInfo.BULLET_tag)
        {
            deathHandler.OnDeath();
            Destroy(gameObject);
        }
    }
}
