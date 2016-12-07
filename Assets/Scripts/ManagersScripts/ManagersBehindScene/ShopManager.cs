using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    private float costForDestroyer = 100.0f;
    private float costForReturn = 1000.0f;
    private float costForRestoreAmmo = 100.0f;
    #region SINGLETON
    private static ShopManager instance = null;
    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                if (!GameObject.FindObjectOfType<ShopManager>())
                {
                    GameObject gameManager = new GameObject(StringNamesInfo.SHOPMANAGER_name);
                    gameManager.AddComponent<ShopManager>();
                    instance = gameManager.GetComponent<ShopManager>();
                }
                else
                {
                    instance = GameObject.FindObjectOfType<ShopManager>();
                }

            }
            return instance;
        }
    }
    #endregion
    #region BUY_operations
    public void BuyDestroyerForMeteoritsItem()
    {
        if (PlayerStats.Current.Money >= costForDestroyer)
        {
            PlayerStats.Current.Money -= costForDestroyer;
            PlayerStats.Current.DestroyerForMeteorits++;
        }
    }
    public void BuyReturnToLifeItem()
    {
        if (PlayerStats.Current.Money >= costForReturn)
        {
            PlayerStats.Current.Money -= costForReturn;
            PlayerStats.Current.ReturnItem++;
        }
    }
    public void BuyRestoreAmmoItem()
    {
        if (PlayerStats.Current.Money >= costForRestoreAmmo)
        {
            PlayerStats.Current.Money -= costForRestoreAmmo;
            PlayerStats.Current.RestoreAmmoItem++;
        }
    }
    #endregion
}
