using UnityEngine;
using LitJson;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class AchievementManagerFillerScript : MonoBehaviour, Manager
{
    public List<AchievementRecordScript> achievements = new List<AchievementRecordScript>();
    int size = 0;
    void Awake()
    {
        size = AchievementManager.Instance.AllAchievements.Count;
    }
    void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        achievements = new List<AchievementRecordScript>(transform.GetComponentsInChildren<AchievementRecordScript>());
        for (int index = 0; index < size; index++)
        {
            achievements[index].Fill(AchievementManager.Instance.AllAchievements[index]);
        }
    }
    int Manager.Size()
    {
        return size;
    }
}
