using UnityEngine;
using System.Collections;

//--Temporary, will be changed after all bullet types will be known---
public enum TypeOfAmmoEnum
{
    NotIdentified,
    Bullet,
    Grenade,
    Ultimate,
}
public class AmmoScript
{
    private int ammoSize;
    private int currentAmmo;
    private int deltaAmmo;
    private TypeOfAmmoEnum typeOfAmmo = TypeOfAmmoEnum.NotIdentified;
    
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
    public TypeOfAmmoEnum TypeOfAmmo
    {
        get { return typeOfAmmo; }
        set { typeOfAmmo = value; }
    }
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
        this.typeOfAmmo = TypeOfAmmoEnum.NotIdentified;
    }
    public AmmoScript(int ammoSize, int currentAmmo, int deltaAmmo, TypeOfAmmoEnum typeOfAmmo)
    {
        this.ammoSize = ammoSize;
        this.currentAmmo = currentAmmo;
        this.deltaAmmo = deltaAmmo;
        this.typeOfAmmo = typeOfAmmo;
    }
    public override string ToString()
    {
        return "System: " + TypeOfAmmo.ToString() + " ammo left: " + CurrentAmmo;
    }
}
