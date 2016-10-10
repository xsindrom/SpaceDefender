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
                    animController.ShowPanel();
                    Time.timeScale = 0.0f;
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
    void Awake()
    {
        animController = gameObject.GetComponent<PanelAnimation>();
    }
}
