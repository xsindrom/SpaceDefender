using UnityEngine;
using System.Collections;

public class ShootSystem : MonoBehaviour
{
    [Header(StringHeadersInfo.OBJECTSTOGENERATE_Header)]
    public GameObject toInstantiate;
    [Header(StringHeadersInfo.OFFSETVECTOR_Header)]
    public Vector3 offset;
    [Header(StringHeadersInfo.AMMOSTATS_Header)]
    public float attackRate;
    public int ammoSize;
    public int deltaAmmo;
    public TypeOfAmmoEnum typeOfAmmoToSet;
    [Header(StringHeadersInfo.KEY_NAME_Header)]
    public string attackKeyName;
    [Header(StringHeadersInfo.MANAGER_Header)]
    public GUIBulletManager bulletManager;
    private Timer timerForActions;
    private AmmoScript ammoStat;
    void Start()
    {
        timerForActions = new Timer(attackRate);
        ammoStat = new AmmoScript(ammoSize, ammoSize, deltaAmmo,typeOfAmmoToSet);
        timerForActions.AddAction(CreateObject);
    }
    public void CreateInfoAboutShoot()
    {
        bulletManager.SetInfo(ammoStat.ToString());
    }
    private void CreateObject()
    {
        if (toInstantiate == null)
        {
            return;
        }
        if (ammoStat.IsAbleToShoot())
        {
            if (Input.GetKey(attackKeyName))
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
}
