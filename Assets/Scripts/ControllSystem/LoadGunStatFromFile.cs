using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadGunStatFromFile : MonoBehaviour
{
    public string jsonName = "";
    private Image powerFullStatImage;
    private Image attackRateStatImage;
    private Image ammoSizeStatImage;
    private Image gunTypeStatImage;

    private Text powerFullStatText;
    private Text attackRateStatText;
    private Text ammoSizeStatText;
    private Text gunTypeStatText;
    void Start()
    {
        powerFullStatImage = GameObject.Find("PowerFullStatToFill").GetComponent<Image>();
        attackRateStatImage = GameObject.Find("AttackRateStatToFill").GetComponent<Image>();
        ammoSizeStatImage = GameObject.Find("AmmoSizeStatToFill").GetComponent<Image>();
        gunTypeStatImage = GameObject.Find("GunTypeStatToFill").GetComponent<Image>();

        powerFullStatText = GameObject.Find("PowerFullStatText").GetComponent<Text>();
        attackRateStatText = GameObject.Find("AttackRateStatText").GetComponent<Text>();
        ammoSizeStatText = GameObject.Find("AmmoSizeStatText").GetComponent<Text>();
        gunTypeStatText = GameObject.Find("GunTypeStatText").GetComponent<Text>();
    }
    public void SetData()
    {
        jsonName = this.gameObject.name;

        GunStats.Instance = new GunStats(jsonName);

        powerFullStatText.text = GunStats.Instance.Powerfull.ToString();
        attackRateStatText.text = GunStats.Instance.AttackRate.ToString();
        ammoSizeStatText.text = GunStats.Instance.AmmoStats.AmmoSize.ToString();
        gunTypeStatText.text = GunStats.Instance.GunType.ToString();


        powerFullStatImage.fillAmount = GunStats.Instance.Difference((float)GunStats.Instance.Powerfull,
                                                                     (float)GunStats.Instance.MaxPowerFull);
        attackRateStatImage.fillAmount = GunStats.Instance.Difference((float)GunStats.Instance.MaxAttackRate,
                                                                      (float)GunStats.Instance.AttackRate);
        ammoSizeStatImage.fillAmount = GunStats.Instance.Difference((float)GunStats.Instance.AmmoStats.AmmoSize,
                                                                    (float)GunStats.Instance.MaxAmmoSize);

        gunTypeStatImage.fillAmount = GunStats.Instance.Difference((float)GunStats.Instance.GunType,
                                                                   (float)GunStats.GunTypeEnum.Strong);
    }

}
