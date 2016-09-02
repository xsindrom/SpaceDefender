using UnityEngine;
using System.Collections;

public class ShootSystem : MonoBehaviour
{
    public GameObject toInstantiate;
    public Vector3 offset;
    public float attackRate;
    //--AmmoStat---
    public int ammoSize;
    public int deltaAmmo;
    //--Key name---
    public string attackKeyName;

    private Timer timerForActions;
    private AmmoScript ammoStat;
    void Start()
    {
        timerForActions = new Timer(attackRate);
        ammoStat = new AmmoScript(ammoSize, ammoSize, deltaAmmo);
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
            if (Input.GetKey(attackKeyName))
            {
                Instantiate(toInstantiate, transform.position + offset, new Quaternion());
                ammoStat.DecreaseAmmo();
            }
        }
    }

    void Update()
    {
        timerForActions.CompleteAction();
    }
}
