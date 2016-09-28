using UnityEngine;
using System.Collections;

public class UnitLifeTime : MonoBehaviour
{
    public int lifeCost;
    private DeathHandler deathHandler;
    void Start()
    {
        deathHandler = new DeathHandler();
        deathHandler.AddAction(delegate { ScoreManager.Instance.AddScore(lifeCost, PlayerStats.Current.ScoreMultipler); });
        deathHandler.AddAction(delegate { PlayerStats.Current.IncreaseLevel(10); });
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
