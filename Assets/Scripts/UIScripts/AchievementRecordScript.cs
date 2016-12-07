using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementRecordScript : MonoBehaviour
{
    private Text[] childText;
    void Start()
    {
        childText = transform.GetComponentsInChildren<Text>();
    }
    public void Fill(AchievementManager.Achievement achievement)
    {
        foreach (Text textUI in childText)
        {
            if (textUI)
            {
                textUI.text = achievement.achievementText;
            }
        }
    }
}
