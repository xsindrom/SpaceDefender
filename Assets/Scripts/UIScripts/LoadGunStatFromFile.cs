using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LitJson;
public class LoadGunStatFromFile : MonoBehaviour
{
    #region FILE_PROCESSING
    public JsonData jData = null;

    private int currentGunIndex = 0;
    public int CurrentGunIndex
    {
        get { return currentGunIndex; }
        set
        {
            if (value >= 0 && value <= jData.Count - 1)
            {
                currentGunIndex = value;
                SetData();
                content.SetRectOffset(new Vector2(0.0f, currentGunIndex * content.rect.height / jData.Count),
                                      new Vector2(0.0f, currentGunIndex * content.rect.height / jData.Count));
            }
            if (value > jData.Count - 1)
            {
                CurrentGunIndex = 0;
            }
            if (value < 0)
            {
                CurrentGunIndex = jData.Count - 1;
            }
        }
    }
    #endregion
    public Image blocker = null;
    private GunStats[] tempStat = null;
    private ScrollRect scrollRect = null;
    private RectTransform content = null;
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
    void Start()
    {
        string gunStatJsonString = Resources.Load<TextAsset>(StringPathsInfo.GUNS_TEXTFILES_ALL_PATH.Replace(".json", "")).text;
        jData = JsonMapper.ToObject(gunStatJsonString);
        tempStat = new GunStats[jData.Count];
        scrollRect = gameObject.GetComponentsInChildren<ScrollRect>()[0];
        content = scrollRect.content;
        for (int index = jData.Count - 1; index >= 0; index--)
        {
            tempStat[index] = JsonUtility.FromJson<GunStats>(jData[index].ToJson());
        }
        int count = 0;
        for (int index = 0; index < tempStat.Length; index++)
        {
            if (PlayerStats.Current.Level >= tempStat[index].LevelToOpen)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        for (int index = count; index < tempStat.Length; index++)
        {
            Image toAdd = Instantiate(blocker) as Image;
            toAdd.transform.SetParent(content.transform);
            toAdd.gameObject.SetRectTransformer(0.0f,content.GetChild(index).GetComponent<RectTransform>().anchorMin.y,
                                                1.0f,content.GetChild(index).GetComponent<RectTransform>().anchorMax.y);
        }
        #region cacheImages
        GameObject powerFullStatGameObjectImage = GameObject.Find(StringNamesInfo.POWERFULLSTAT_image_name);
        if(powerFullStatGameObjectImage)
        {
            powerFullStatImage = powerFullStatGameObjectImage.GetComponent<Image>();
        }
        GameObject attackRateStatGameObjectImage = GameObject.Find(StringNamesInfo.ATTACKRATESTAT_image_name);
        if (attackRateStatGameObjectImage)
        {
            attackRateStatImage = attackRateStatGameObjectImage.GetComponent<Image>();
        }
        GameObject ammoSizeStatGameObjectImage = GameObject.Find(StringNamesInfo.AMMOSIZESTAT_image_name);
        if (ammoSizeStatGameObjectImage)
        {
            ammoSizeStatImage = ammoSizeStatGameObjectImage.GetComponent<Image>();
        }
        GameObject gunTypeStatGameObjectImage = GameObject.Find(StringNamesInfo.GUNTYPESTAT_image_name);
        if (gunTypeStatGameObjectImage)
        {
            gunTypeStatImage = gunTypeStatGameObjectImage.GetComponent<Image>();
        }
        #endregion
        #region cacheTexts
        GameObject powerFullStatGameObjectText = GameObject.Find(StringNamesInfo.POWERFULLSTAT_text_name);
        if (powerFullStatGameObjectText)
        {
            powerFullStatText = powerFullStatGameObjectText.GetComponent<Text>();
        }
        GameObject attackRateStatGameObjectText = GameObject.Find(StringNamesInfo.ATTACKRATESTAT_text_name);
        if (attackRateStatGameObjectText)
        {
            attackRateStatText = attackRateStatGameObjectText.GetComponent<Text>();
        }
        GameObject ammoSizeStatGameObjectText = GameObject.Find(StringNamesInfo.AMMOSIZESTAT_text_name);
        if (ammoSizeStatGameObjectText)
        {
            ammoSizeStatText = ammoSizeStatGameObjectText.GetComponent<Text>();
        }
        GameObject gunTypeStatGameObjectText = GameObject.Find(StringNamesInfo.GUNTYPESTAT_text_name);
        if (gunTypeStatGameObjectText)
        {
            gunTypeStatText = gunTypeStatGameObjectText.GetComponent<Text>();
        }
        #endregion
        SetData();
    }
    #region LOGIC
    public void SetData()
    {
        GunStats toSet = tempStat[currentGunIndex];
        #region fillingTexts
        if (powerFullStatText)
        {
            powerFullStatText.text = toSet.Powerfull.ToString();
        }
        if (attackRateStatText)
        {
            attackRateStatText.text = toSet.AttackRate.ToString();
        }
        if (ammoSizeStatText)
        {
            ammoSizeStatText.text = toSet.AmmoStats.AmmoSize.ToString();
        }
        if (gunTypeStatText)
        {
            gunTypeStatText.text = toSet.GunType.ToString();
        }
        #endregion
        #region fillingImages
        if (powerFullStatImage)
        {
            powerFullStatImage.fillAmount = toSet.Difference((float)toSet.Powerfull, (float)toSet.MaxPowerFull);
        }
        if (attackRateStatImage)
        {
            attackRateStatImage.fillAmount = toSet.Difference((float)toSet.MaxAttackRate, (float)toSet.AttackRate);
        }
        if (ammoSizeStatImage)
        {
            ammoSizeStatImage.fillAmount = toSet.Difference((float)toSet.AmmoStats.AmmoSize, (float)toSet.MaxAmmoSize);
        }
        if (gunTypeStatImage)
        {
            gunTypeStatImage.fillAmount = toSet.Difference((float)toSet.GunType, (float)GunStats.GunTypeEnum.Ultimate);
        }
        #endregion
    }
    public void SetGun()
    {
        if (tempStat[currentGunIndex].LevelToOpen <= PlayerStats.Current.Level)
        {
            GunStats.Instance = tempStat[currentGunIndex];
            GunRendererScript.indexToSet = GunStats.Instance.Id;
        }
    }
    public void NextGun()
    {
        CurrentGunIndex++;
    }
    public void PreviousGun()
    {
        CurrentGunIndex--;
    }
    
    #endregion
}
