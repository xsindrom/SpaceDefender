using UnityEngine;
using LitJson;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GunStats
{
    public enum GunTypeEnum { NotIdentified, Weak, Normal, Strong }
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
    private string gunStatJsonString;
    private JsonData gunStatJData;

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
    [SerializeField]
    public static double scoreMultipler = 1.0f;
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

    public float Difference(float less, float bigger)
    {
        return less / bigger;
    }

    public GunStats()
    {

    }
    public GunStats(string jsonName)
    {
        LoadDataFromJSON(jsonName);
    }
    public void LoadDataFromJSON(string jsonName)
    {
        string asset = jsonName.Replace(".json", "");
        gunStatJsonString = Resources.Load<TextAsset>(StringPathsInfo.GUNS_TEXTFILES_PATH + asset).text;
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
        Debug.Log(this.maxPowerFull);
    }
    private void SaveDataToJSON(string jsonName)
    {
        File.WriteAllText(StringPathsInfo.RESOURCES_PATH + jsonName, JsonUtility.ToJson(this, true));
    }
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
               "GunType: " + GunType.ToString() + "\n" +
               "scoreMultipler: " + scoreMultipler + "\n";
    }
}
