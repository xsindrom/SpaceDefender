using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using LitJson;
public interface Manager
{
    int Size();
}
public class LeadersManagerScript : MonoBehaviour, Manager
{
    public string jsonName;
    public JsonData jDataList;
    public List<LeaderRecordScript> recordsList = new List<LeaderRecordScript>();
    int size = 0;
    void Awake()
    {
        jsonName = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
        jDataList = new JsonData();
        jDataList.SetJsonType(JsonType.Array);
        string jString = File.ReadAllText(jsonName);
        if (jString.Length != 0)
        {
            jDataList = JsonMapper.ToObject(jString);
        }
        size = jDataList.Count;
    }
    void Start()
    {
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        recordsList = new List<LeaderRecordScript>(transform.GetComponentsInChildren<LeaderRecordScript>());
        for (int index = 0; index < size; index++)
        {
            recordsList[index].Fill(ParseLeader(index, jDataList));
        }
    } 
    int Manager.Size()
    {
        return size;
    }
    private LeaderRecordScript.Leader ParseLeader(int index, JsonData jData)
    {
        int id = int.Parse(jData[index]["id"].ToString());
        string name = jData[index]["name"].ToString();
        int score = int.Parse(jData[index]["score"].ToString());
        int level = int.Parse(jData[index]["level"].ToString());

        return new LeaderRecordScript.Leader(id,name,score,level);
    } 
}
