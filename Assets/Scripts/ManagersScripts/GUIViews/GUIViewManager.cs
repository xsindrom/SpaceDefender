using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum TypeOfManager
{
    NotIdentified,
    Score,
    Achieve
}
public class GUIViewManager : MonoBehaviour
{
    [Header(StringHeadersInfo.MANAGER_Header)]
    public TypeOfManager typeOfManager;
    private Text textToView;
    private string TextToView
    {
        get { return textToView.text; }
        set
        {
            if (textToView.text != value)
            {
                textToView.text = value;
            }
        }
    }
    private IManager manager;

    void Start()
    {
        SetTypeOfManager();
        textToView = transform.GetComponent<Text>();
        TextToView = manager.GetInfo();
    }
    void Update()
    {
        TextToView = manager.GetInfo();
    }
    void SetTypeOfManager()
    {
        switch (typeOfManager)
        {
            case TypeOfManager.Score: manager = ScoreManager.Instance; break;
            case TypeOfManager.Achieve: manager = AchievementManager.Instance; break;
            case TypeOfManager.NotIdentified: manager = null; break;
        }
    }
}