using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    #region TEXTS_TO_FILL
    public Text[] levelText;
    public Text[] scoreText;
    public Text[] scoreMultiplerText;
    public Text[] ammoText;
    #endregion
    #region FIELD_TO_PROCESS
    [SerializeField]
    private int levelToSet = 0;
    [SerializeField]
    private int scoreToSet = 0;
    [SerializeField]
    private int scoreMultiplerToSet = 0;
    [SerializeField]
    private int ammoToSet = 0;
    #endregion
    #region Properties
    public int LevelToSet
    {
        get { return levelToSet; }
        set
        {
            if (value != levelToSet)
            {
                levelToSet = value;
                if (levelText != null)
                {
                    levelText.SetText(levelToSet.ToString());
                }
            }
        }
    }
   
    public int ScoreToSet
    {
        get { return scoreToSet; }
        set
        {
            if (value != scoreToSet)
            {
                scoreToSet = value;
                if (scoreText != null)
                {
                    scoreText.SetText(scoreToSet.ToString());
                }
            }
        }
    }
   
    public int ScoreMultiplerToSet
    {
        get { return scoreMultiplerToSet; }
        set
        {
            if (value != scoreMultiplerToSet)
            {
                scoreMultiplerToSet = value;
                if (scoreMultiplerText != null)
                {
                    scoreMultiplerText.SetText(scoreMultiplerToSet.ToString());
                }
            }
        }
    }
   
    public int AmmoToSet
    {
        get { return ammoToSet; }
        set
        {
            if (value != ammoToSet)
            {
                ammoToSet = value;
                if (ammoText != null)
                {
                    ammoText.SetText(ammoToSet + " / " + GunStats.Instance.AmmoStats.AmmoSize);
                }
            }
        }
    }
    #endregion
    #region SINGLETON
    private static GUIManager instance;
    public static GUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                if (!GameObject.FindObjectOfType<GUIManager>())
                {
                    GameObject guiManager = new GameObject("GUIManager");
                    guiManager.AddComponent<GUIManager>();
                    instance = guiManager.GetComponent<GUIManager>();
                    instance.FindGUIObjectsOnClick();
                }
                else
                {
                    instance = GameObject.FindObjectOfType<GUIManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    #region STANDART_EVENTS
    void Start()
    {
        FindGUIObjectsOnClick();
        FillTextFieldOnClick();
    }
    #endregion
    #region LOGIC
    public void FillTextFieldOnClick()
    {
        levelText.SetText(levelToSet.ToString());
        scoreText.SetText(scoreToSet.ToString());
        scoreMultiplerText.SetText(scoreMultiplerToSet.ToString());
        ammoText.SetText(ammoToSet.ToString());
    }
    public void FindGUIObjectsOnClick()
    {
        RefreshStats();
        GameObject[] levelObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.LEVELINFO_tag);
        GameObject[] scorelObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.SCOREINFO_tag);
        GameObject[] scoreMultiplerObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.SCOREMULTIPLER_tag);
        GameObject[] ammoObjects = GameObject.FindGameObjectsWithTag(StringNamesInfo.AMMOINFO_tag);
        levelText = new Text[levelObjects.Length];
        scoreText = new Text[scorelObjects.Length];
        scoreMultiplerText = new Text[scoreMultiplerObjects.Length];
        ammoText = new Text[ammoObjects.Length];

        if (levelObjects == null || scorelObjects == null || scoreMultiplerObjects == null || ammoObjects == null)
        {
            return;
        }
       
        levelText.CacheComponents<Text>(levelObjects);
        scoreText.CacheComponents<Text>(scorelObjects);
        scoreMultiplerText.CacheComponents<Text>(scoreMultiplerObjects);
        ammoText.CacheComponents<Text>(ammoObjects);
    }

    public void RefreshStats()
    {
        LevelToSet = PlayerStats.Current.Level;
        ScoreToSet = ScoreManager.Instance.Score;
        ScoreMultiplerToSet = PlayerStats.Current.ScoreMultipler;
        AmmoToSet = GunStats.Instance.AmmoStats.CurrentAmmo;
    }
    #endregion
}
