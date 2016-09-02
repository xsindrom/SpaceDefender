using UnityEngine;
using System.Collections;

public class AmmoScript
{
    private int ammoSize;
    private int currentAmmo;
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
            if (value > 0 && value < ammoSize) { deltaAmmo = value; }
        }
    }
    public bool IsAbleToShoot()
    {
        return (currentAmmo > 0 && currentAmmo <= ammoSize);
    }
    public void DecreaseAmmo()
    {
        CurrentAmmo -= DeltaAmmo;
    }
    public AmmoScript()
    {
        this.ammoSize = 0;
        this.currentAmmo = 0;
        this.deltaAmmo = 0;
    }
    public AmmoScript(int ammoSize,int currentAmmo,int deltaAmmo)
    {
        this.ammoSize = ammoSize;
        this.currentAmmo = currentAmmo;
        this.deltaAmmo = deltaAmmo;
    }
}
