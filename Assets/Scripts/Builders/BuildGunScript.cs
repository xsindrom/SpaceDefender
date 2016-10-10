using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class BuildGunScript : MonoBehaviour
{
    [Header(StringHeadersInfo.OBJECTSTOGENERATE_Header)]
    public GameObject toInstatiate;
    #region GUN_STATS
    private GunStats gunStats;
    public GunStats GunStat
    {
        get { return gunStats; }
    }
    #endregion
    #region SHOOT_SYSTEM
    private ShootSystem shootSystem;
    #endregion
    #region CONTROLL_SYSTEM
    private ControllGunSystem controllGunSystem;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
       
        gunStats = GunStats.Instance;

        shootSystem = transform.GetComponent<ShootSystem>();
        shootSystem.ToInstantiate = toInstatiate;
        shootSystem.AttackRate = (float)gunStats.AttackRate;
        shootSystem.AmmoStats = gunStats.AmmoStats;

        controllGunSystem = transform.GetComponent<ControllGunSystem>();
        controllGunSystem.MinAngle = (float)gunStats.MinAngle;
        controllGunSystem.MaxAngle = (float)gunStats.MaxAngle;

    }
    #endregion
}
