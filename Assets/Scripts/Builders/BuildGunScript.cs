using UnityEngine;
using System;
using System.Collections;

public class BuildGunScript : MonoBehaviour
{
    [Header(StringHeadersInfo.OBJECTSTOGENERATE_Header)]
    public GameObject toInstatiate;
    private GunStats gunStats;
    public GunStats GunStat
    {
        get { return gunStats; }
    }
    private ShootSystem shootSystem;
    private ControllGunSystem controllGunSystem;

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

}
