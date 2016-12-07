using UnityEngine;
using System;
using System.Collections;
[Serializable]
public class AmmoStat
{
    [SerializeField]
    private int ammoSize;
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private int deltaAmmo;
    public int AmmoSize
    {
        get { return ammoSize; }
        set
        {
            if (value < 0) { ammoSize = 0; }
            ammoSize = value;
        }
    }
    public int CurrentAmmo
    {
        get { return currentAmmo; }
        set
        {
            if (value >= 0 && value <= ammoSize) { currentAmmo = value; }
            if (value < 0) { currentAmmo = 0; }
            if (value > ammoSize) { currentAmmo = ammoSize; }
        }
    }
    public int DeltaAmmo
    {
        get { return deltaAmmo; }
        set
        {
            if (value >= 0 && value <= ammoSize) { deltaAmmo = value; }
        }
    }
    #region LOGIC
    public bool IsAbleToShoot()
    {
        return (currentAmmo > 0 && currentAmmo <= ammoSize);
    }
    public void DecreaseAmmo(int ammoToDecrease, VoidDelegate action)
    {
        CurrentAmmo -= ammoToDecrease;
        if (action != null) { action(); }
    }
    public void IncreaseAmmo(int ammoToAdd, VoidDelegate action)
    {
        CurrentAmmo += ammoToAdd;
        if (action != null) { action(); }
    }
    #endregion
    #region CONSTRUCTORS
    public AmmoStat()
    {
        this.ammoSize = 0;
        this.currentAmmo = 0;
        this.deltaAmmo = 0;
    }
    public AmmoStat(int ammoSize,int currentAmmo,int deltaAmmo)
    {
        this.ammoSize = ammoSize;
        this.currentAmmo = currentAmmo;
        this.deltaAmmo = deltaAmmo;
    }
    #endregion
    public override string ToString()
    {
        return CurrentAmmo + "/" +AmmoSize;
    }
}
