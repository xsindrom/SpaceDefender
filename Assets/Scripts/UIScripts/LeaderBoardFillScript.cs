using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Collections;
using System.Text;
public class LeaderBoardFillScript : MonoBehaviour
{
    public Text[] whereToDisplay;
    public GameObject prefabToInstatiate;
    public GameObject[] childrenRecords;
    public GameObject[] childRecordColumns;
    public static string jsonName;
    public static JsonData jDataList;
    void Awake()
    {

        jsonName = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
        if (!File.Exists(jsonName))
        {
            File.Create(jsonName);
        }
    }
    void InitJData()
    {
        if (jDataList == null)
        {
            jDataList = new JsonData();
            jDataList.SetJsonType(JsonType.Array);
            if (File.Exists(jsonName))
            {
                string jString = File.ReadAllText(jsonName);
                jDataList = JsonMapper.ToObject(jString);
            }
        }
    }
    void Start()
    {
        InitJData();
        int size = jDataList.Count;
        PlayerStats[] leaders = new PlayerStats[size];
        childrenRecords = new GameObject[size];
        childRecordColumns = new GameObject[4];
        gameObject.SetRectTransformer(0.0f, -size * 0.05f, 1.0f, 1.0f);
        for (int index = 0; index < size; index++)
        {
            leaders[index] = new PlayerStats(index, jsonName);
            childrenRecords[index] = Instantiate(prefabToInstatiate) as GameObject;
            FillText(0, index.ToString(), childrenRecords[index]);
            FillText(1, leaders[index].Name, childrenRecords[index]);
            FillText(2, leaders[index].Score.ToString(), childrenRecords[index]);
            FillText(3, leaders[index].Level.ToString(), childrenRecords[index]);
        }
        childrenRecords.SetParentToObjects(this.gameObject);
    }
    private void FillText(int index, string whatToFill, GameObject parent)
    {
        parent.GetChilds()[index].GetComponent<Text>().text = whatToFill;
    }
   
    
}
