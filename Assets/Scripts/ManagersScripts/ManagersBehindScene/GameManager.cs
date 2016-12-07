using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool isEnd = false;
    public int health = 100;
    public int Health
    {
        get { return health; }
        set
        {
            health = (value >= 0) ? value: 0;
            isEnd = (health == 0) ? true : false;
            if (isEnd)
            {
                if (animController)
                {
                    if (PanelAnimation.isPlayed)
                    {
                        PanelAnimation.HideCurrentPanel();
                    }

                    animController.ShowPanel();    
                    Time.timeScale = 0.0f;
                    GUIManager.Instance.FindGUIObjectsOnClick();
                    GUIManager.Instance.FillTextFieldOnClick();
                }
            }
            GUIManager.Instance.HealthToSet = health;
        }
    }
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                if (!GameObject.FindObjectOfType<GameManager>())
                {
                    GameObject gameManager = new GameObject(StringNamesInfo.GAMEMANAGER_name);
                    gameManager.AddComponent<GameManager>();
                    instance = gameManager.GetComponent<GameManager>();
                }
                else
                {
                    instance = GameObject.FindObjectOfType<GameManager>();
                }

            }
            return instance;
        }
    }
    private PanelAnimation animController = null;
    public int destroyerForMeteorits = 0;
    public int returnItem = 0;
    public int restoreAmmoItem = 0;
    void Awake()
    {
        animController = gameObject.GetComponent<PanelAnimation>();
    }
    public void DestroyAllMeteorits()
    {
        if (destroyerForMeteorits > 0)
        {
            GameObject[] meteorits = GameObject.FindGameObjectsWithTag(StringNamesInfo.METEORIT_tag);
            UnitLifeTime[] units = new UnitLifeTime[meteorits.Length];
            units.CacheComponents<UnitLifeTime>(meteorits);
            for (int index = 0; index < meteorits.Length; index++)
            {
                units[index].deathHandler.OnDeath();
                StartCoroutine(units[index].Death(StringNamesInfo.EXPLODE_inAir_animation_name));
            }
            destroyerForMeteorits--;
            PlayerStats.Current.DestroyerForMeteorits = destroyerForMeteorits;
        }
    }
    public void ReturnToLife()
    {
        if (returnItem > 0)
        {
            PanelAnimation.HideCurrentPanel();
            Time.timeScale = 1.0f;
            health = 100;
            destroyerForMeteorits++;
            DestroyAllMeteorits();
            returnItem--;
            PlayerStats.Current.ReturnItem = returnItem;
        }
    }
    public void RestoreAmmo()
    {
        if (restoreAmmoItem > 0)
        {
            GunStats.Instance.AmmoStats.CurrentAmmo = GunStats.Instance.AmmoStats.AmmoSize;
            restoreAmmoItem--;
            PlayerStats.Current.RestoreAmmoItem = restoreAmmoItem;
        }
    }
}
