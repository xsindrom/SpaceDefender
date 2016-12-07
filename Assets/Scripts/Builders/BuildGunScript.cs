using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Sprite baseSprite;
    public Sprite barrelSprite;
    private ShootSystem shootSystem;
    public ShootSystem ShootSystem
    {
        get { return shootSystem; }
    }
    private ControllGunSystem controllGunSystem;
    public ControllGunSystem ControllGunSystem
    {
        get { return controllGunSystem; }
    }
    void Awake()
    {
        baseSprite = GunRendererScript.baseSprite[GunRendererScript.indexToSet];
        barrelSprite = GunRendererScript.barrelSprite[GunRendererScript.indexToSet];
        if (baseSprite)
        {
            transform.GetComponentInParent<SpriteRenderer>().sprite = baseSprite;
        }
        if (barrelSprite)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = barrelSprite;
        }
        gunStats = GunStats.Instance;

        shootSystem = transform.FindChild("FirePoint").GetComponent<ShootSystem>();
        shootSystem.ToInstantiate = toInstatiate;
        shootSystem.AttackRate = (float)gunStats.AttackRate;
        shootSystem.AmmoStats = gunStats.AmmoStats;
        shootSystem.transform.localPosition = new Vector2(barrelSprite.rect.height / (Mathf.Abs(transform.parent.position.y)), 0.0f);
        controllGunSystem = transform.GetComponent<ControllGunSystem>();
        controllGunSystem.MinAngle = (float)gunStats.MinAngle;
        controllGunSystem.MaxAngle = (float)gunStats.MaxAngle;
    }
}
