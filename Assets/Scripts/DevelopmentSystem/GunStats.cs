using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GunStats
{
    public enum GunTypeEnum { NotIdentified, Weak, Normal, Strong }
    #region FIELDS
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
    //--Read From---
    [SerializeField]
    #endregion
    
    #region PROPERTIES
    public string GunName
    {
        get { return gunName; }
        private set
        {
            gunName = "empty";
            if (value != null)
            {
                gunName = value;
            }
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
    #endregion
    #region SINGLETON
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
    #endregion
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
    public GunStats(string jsonName) { LoadDataFromJSON(jsonName); }
    #endregion
    #region FILE_PROCESSING_PART
    private string gunStatJsonString;
    private JsonData gunStatJData;
    public void LoadDataFromJSON(string jsonName)
    {
        #region GET_DATA
        string asset = jsonName.Replace(".json", "");
        gunStatJsonString = Resources.Load<TextAsset>(StringPathsInfo.GUNS_TEXTFILES_PATH + asset).text;
        if (gunStatJsonString.Length == 0) { return; }
        #endregion
        #region PARSE_DATA
        this.GunName = JsonUtility.FromJson<GunStats>(gunStatJsonString).GunName;
        this.AmmoStats.AmmoSize = JsonUtility.FromJson<GunStats>(gunStatJsonString).AmmoStats.AmmoSize;
        this.AmmoStats.CurrentAmmo = JsonUtility.FromJson<GunStats>(gunStatJsonString).AmmoStats.CurrentAmmo;
        this.AmmoStats.DeltaAmmo = JsonUtility.FromJson<GunStats>(gunStatJsonString).AmmoStats.DeltaAmmo;
        this.AttackRate = JsonUtility.FromJson<GunStats>(gunStatJsonString).AttackRate;
        this.MinAngle = JsonUtility.FromJson<GunStats>(gunStatJsonString).MinAngle;
        this.MaxAngle = JsonUtility.FromJson<GunStats>(gunStatJsonString).MaxAngle;
        this.Powerfull = JsonUtility.FromJson<GunStats>(gunStatJsonString).Powerfull;
        this.GunType = JsonUtility.FromJson<GunStats>(gunStatJsonString).GunType;
        this.maxAmmoSize = JsonUtility.FromJson<GunStats>(gunStatJsonString).MaxAmmoSize;
        this.maxPowerFull = JsonUtility.FromJson<GunStats>(gunStatJsonString).MaxPowerFull;
        this.maxAttackRate = JsonUtility.FromJson<GunStats>(gunStatJsonString).MaxAttackRate;
        this.levelToOpen = JsonUtility.FromJson<GunStats>(gunStatJsonString).LevelToOpen;
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
}
