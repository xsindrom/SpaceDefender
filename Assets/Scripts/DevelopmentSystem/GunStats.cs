using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GunStats
{
    public enum GunTypeEnum { NotIdentified, Weak, Normal, Strong, Ultimate }
    #region FIELDS
    [SerializeField]
    private int id;
    [SerializeField]
    private string gunName;
    [SerializeField]
    private GunTypeEnum gunType;
    //--BulletInformation---
    [SerializeField]
    private double powerfull;
    //--ShootInformation---
    [SerializeField]
    private double attackRate;
    //--Limits for ControllGunSystem
    [SerializeField]
    private double minAngle;
    [SerializeField]
    private double maxAngle;
    //--AmmoStat---
    [SerializeField]
    private AmmoStat ammoStats;
    #endregion
    
    #region PROPERTIES
    public string GunName
    {
        get { return gunName; }
        private set
        {
            gunName = value;
        }
    }
    public AmmoStat AmmoStats
    {
        get
        {
            if (ammoStats == null)
            {
                ammoStats = new AmmoStat();
            }
            return ammoStats;
        }
        private set
        {
            if (value != null)
            {
                ammoStats = value;
            }
        }
    }
    public double AttackRate
    {
        get { return attackRate; }
        private set
        {
            attackRate = value;
        }
    }
    public double Powerfull
    {
        get { return powerfull; }
        private set
        {
            powerfull = value;
        }
    }
    public double MinAngle
    {
        get { return minAngle; }
        private set
        {
            minAngle = value;
        }
    }
    public double MaxAngle
    {
        get { return maxAngle; }
        private set
        {
            maxAngle = value;
        }
    }
    public GunTypeEnum GunType
    {
        get { return gunType; }
        private set
        {
            gunType = (value != GunTypeEnum.NotIdentified) ? value : GunTypeEnum.NotIdentified;
        }
    }
    public int Id
    {
        get { return id; }
        private set
        {
            id = value;
        }
    }
    #endregion
    [NonSerialized]
    private static GunStats instance;
    public static GunStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GunStats();
            }
            return instance;
        }
        set
        {
            if (value != null)
            {
                instance = value;
            }
        }
    }
    #region MESSURES
    [SerializeField]
    private double maxPowerFull;
    public double MaxPowerFull
    {
        get { return maxPowerFull; }
    }
    [SerializeField]
    private double maxAttackRate;
    public double MaxAttackRate
    {
        get { return maxAttackRate; }
    }
    [SerializeField]
    private int maxAmmoSize;
    public int MaxAmmoSize
    {
        get { return maxAmmoSize; }
    }
    [SerializeField]
    private int levelToOpen;
    public int LevelToOpen
    {
        get { return levelToOpen; }
    }
    public float Difference(float less, float bigger)
    {
        return less / bigger;
    }
    #endregion
    #region CONSTRUCTORS
    public GunStats() { }
    public GunStats(GunStats gunStat)
    {
        if (gunStat == null)
        {
            return;
        }
        this.id = gunStat.id;
        this.gunName = gunStat.gunName;
        this.AmmoStats.AmmoSize = gunStat.AmmoStats.AmmoSize;
        this.AmmoStats.CurrentAmmo = gunStat.AmmoStats.CurrentAmmo;
        this.AmmoStats.DeltaAmmo = gunStat.AmmoStats.DeltaAmmo;
        this.attackRate = gunStat.attackRate;
        this.minAngle = gunStat.minAngle;
        this.maxAngle = gunStat.maxAngle;
        this.powerfull = gunStat.powerfull;
        this.gunType = gunStat.gunType;
        this.maxAmmoSize = gunStat.maxAmmoSize;
        this.maxPowerFull = gunStat.maxPowerFull;
        this.maxAttackRate = gunStat.maxAttackRate;
        this.levelToOpen = gunStat.levelToOpen;
    }
    public GunStats(string jsonName) { LoadDataFromJSON(jsonName); }
    public static GunStats Empty
    {
        get
        {
            AmmoStat emptyAmmoStats = new AmmoStat();
            GunStats emptyStats = new GunStats();
            emptyStats.ammoStats = emptyAmmoStats;
            emptyStats.GunType = GunTypeEnum.NotIdentified;
            return emptyStats;
        }
    }
    #endregion
    #region FILE_PROCESSING_PART
    private string gunStatJsonString;
    private JsonData gunStatJData;
    public void LoadDataFromJSON(string jsonName)
    {
        string asset = jsonName.Replace(".json", "");
        gunStatJsonString = Resources.Load<TextAsset>(StringPathsInfo.GUNS_TEXTFILES_PATH + asset).text;
        if (gunStatJsonString.Length == 0) { return; }
        #region PARSE_DATA
        GunStats gunStat = JsonUtility.FromJson<GunStats>(gunStatJsonString);
        if (gunStat.Equals(GunStats.Empty))
        {
            return;
        }
        this.id = gunStat.Id;
        this.GunName = gunStat.GunName;
        this.AmmoStats.AmmoSize = gunStat.AmmoStats.AmmoSize;
        this.AmmoStats.CurrentAmmo = gunStat.AmmoStats.CurrentAmmo;
        this.AmmoStats.DeltaAmmo = gunStat.AmmoStats.DeltaAmmo;
        this.AttackRate = gunStat.AttackRate;
        this.MinAngle = gunStat.MinAngle;
        this.MaxAngle = gunStat.MaxAngle;
        this.Powerfull = gunStat.Powerfull;
        this.GunType = gunStat.GunType;
        this.maxAmmoSize = gunStat.MaxAmmoSize;
        this.maxPowerFull = gunStat.MaxPowerFull;
        this.maxAttackRate = gunStat.MaxAttackRate;
        this.levelToOpen = gunStat.LevelToOpen;
        
        #endregion
    }
    private void SaveDataToJSON(string jsonName)
    {
        File.WriteAllText(StringPathsInfo.RESOURCES_PATH + jsonName, JsonUtility.ToJson(this, true));
    }
    #endregion
    public override string ToString()
    {
        return "GunName" + gunName + "\n" +
               "AmmoStats:{\n" +
               "AmmoSize: " + AmmoStats.AmmoSize + "\n" +
               "CurrentAmmo: " + AmmoStats.CurrentAmmo + "\n" +
               "DeltaAmmo: " + AmmoStats.DeltaAmmo + "\n" +
               "AttackRate: " + AttackRate + "\n" +
               "StartSpeed: " + Powerfull + "\n" +
               "MinAngle: " + MinAngle + "\n" +
               "MaxAngle: " + MaxAngle + "\n" +
               "GunType: " + GunType.ToString() + "\n";
    }
    public override bool Equals(object obj)
    {
        if (obj == null) { return false; }
        GunStats toCompare = (GunStats)obj;
        if (toCompare == null) { return false; }
        if (this.ammoStats.AmmoSize == toCompare.ammoStats.AmmoSize)
            if (this.ammoStats.CurrentAmmo == toCompare.ammoStats.CurrentAmmo)
                if (this.ammoStats.DeltaAmmo == toCompare.ammoStats.DeltaAmmo)
                    if (this.attackRate == toCompare.attackRate)
                        if (this.powerfull == toCompare.powerfull)
                            if (this.minAngle == toCompare.minAngle)
                                if (this.maxAngle == toCompare.maxAngle)
                                    if (this.gunType == toCompare.gunType)
                                            return true;
        return false;
    }
    public override int GetHashCode()
    {
        int toReturn = 0;
        toReturn += ammoStats.AmmoSize;
        toReturn += ammoStats.CurrentAmmo;
        toReturn += ammoStats.DeltaAmmo;
        toReturn += (int)gunType;
        return toReturn;
    }
}
