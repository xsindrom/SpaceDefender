using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
public class ShootSystem : MonoBehaviour
{
    private GameObject toInstantiate;
    public GameObject ToInstantiate
    {
        get { return ToInstantiate; }
        set
        {
            if (value != null)
            {
                toInstantiate = value;
            }
        }
    }
    [Header(StringHeadersInfo.OFFSETVECTOR_Header)]
    public Vector3 offset;
    [Header(StringHeadersInfo.AMMOSTATS_Header)]
    private float attackRate;
    [Header(StringHeadersInfo.MANAGER_Header)]
    public GUIBulletManager bulletManager;

    private Timer timerForActions;
    private AmmoStat ammoStat;

    public AmmoStat AmmoStats
    {
        private get { return ammoStat; }
        set
        {
            if (value != null)
            {
                ammoStat = value;
            }
        }
    }
    public float AttackRate
    {
        get { return attackRate; }
        set
        {
            if (value > 0)
            {
                attackRate = value;
            }
        }
    }
    public void CreateInfoAboutShoot()
    {
        bulletManager.SetInfo(ammoStat.ToString());
    }
#if UNITY_STANDALONE_WIN
    void Start()
    {
        timerForActions = new Timer(attackRate);
        timerForActions.AddAction(CreateObject);
    }
    private void CreateObject()
    {
        if (toInstantiate == null)
        {
            return;
        }
        if (ammoStat.IsAbleToShoot())
        {

            if (Input.GetButton("Fire"))
            {
                Instantiate(toInstantiate, transform.position + offset, new Quaternion());
                ammoStat.DecreaseAmmo(ammoStat.DeltaAmmo,CreateInfoAboutShoot);
            }
        }
    }
    void Update()
    {
        timerForActions.CompleteAction();
    }
#endif
#if UNITY_ANDROID

    public void Shoot()
    {
        if (toInstantiate == null)
        {
            return;
        }
        if (ammoStat.IsAbleToShoot())
        {
            Instantiate(toInstantiate, transform.position + offset, new Quaternion());
            ammoStat.DecreaseAmmo(ammoStat.DeltaAmmo, CreateInfoAboutShoot);
        }
    }
    
#endif
}
