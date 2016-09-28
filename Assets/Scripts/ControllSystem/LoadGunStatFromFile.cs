using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGunStatFromFile : MonoBehaviour
{
    public string jsonName = "";
    #region IMAGE_FIELDS_TO_CACHE
    private Image powerFullStatImage;
    private Image attackRateStatImage;
    private Image ammoSizeStatImage;
    private Image gunTypeStatImage;
    #endregion
    #region TEXT_FIELDS_TO_CACHE
    private Text powerFullStatText;
    private Text attackRateStatText;
    private Text ammoSizeStatText;
    private Text gunTypeStatText;
    #endregion
    #region STANDART_EVENTS
    void Start()
    {
        #region cacheImages
            powerFullStatImage = GameObject.Find(StringNamesInfo.POWERFULLSTAT_image_name).GetComponent<Image>();
            attackRateStatImage = GameObject.Find(StringNamesInfo.ATTACKRATESTAT_image_name).GetComponent<Image>();
            ammoSizeStatImage = GameObject.Find(StringNamesInfo.AMMOSIZESTAT_image_name).GetComponent<Image>();
            gunTypeStatImage = GameObject.Find(StringNamesInfo.GUNTYPESTAT_image_name).GetComponent<Image>();
        #endregion
        #region cacheTexts
            powerFullStatText = GameObject.Find(StringNamesInfo.POWERFULLSTAT_text_name).GetComponent<Text>();
            attackRateStatText = GameObject.Find(StringNamesInfo.ATTACKRATESTAT_text_name).GetComponent<Text>();
            ammoSizeStatText = GameObject.Find(StringNamesInfo.AMMOSIZESTAT_text_name).GetComponent<Text>();
            gunTypeStatText = GameObject.Find(StringNamesInfo.GUNTYPESTAT_text_name).GetComponent<Text>();
        #endregion
    }
    #endregion
    #region LOGIC
    public void SetData()
    {
        jsonName = this.gameObject.name;
        GunStats toSet = new GunStats(jsonName);
        #region fillingTexts
            powerFullStatText.text = toSet.Powerfull.ToString();
            attackRateStatText.text = toSet.AttackRate.ToString();
            ammoSizeStatText.text = toSet.AmmoStats.AmmoSize.ToString();
            gunTypeStatText.text = toSet.GunType.ToString();
        #endregion
        #region fillingImages
            powerFullStatImage.fillAmount = toSet.Difference((float)toSet.Powerfull, (float)toSet.MaxPowerFull);
            attackRateStatImage.fillAmount = toSet.Difference((float)toSet.MaxAttackRate, (float)toSet.AttackRate);
            ammoSizeStatImage.fillAmount = toSet.Difference((float)toSet.AmmoStats.AmmoSize, (float)toSet.MaxAmmoSize);
            gunTypeStatImage.fillAmount = toSet.Difference((float)toSet.GunType, (float)GunStats.GunTypeEnum.Strong);
        #endregion
        if (toSet.LevelToOpen <= PlayerStats.Current.Level)
        {
            GunStats.Instance = toSet;
        }
    }
    #endregion
}
