using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Collections;
using System.Text;
public class LeaderBoardFillScript : MonoBehaviour
{
    #region FIELDS
    public GameObject prefabToInstatiate;

    private Text[] whereToDisplay;
    private GameObject[] childrenRecords;
    #endregion
    #region FILE_PROCESSING_PART
    public static string jsonName;
    public static JsonData jDataList;
    void InitJData()
    {
        if (jDataList == null)
        {
            jDataList = new JsonData();
            jDataList.SetJsonType(JsonType.Array);
            string jString = File.ReadAllText(jsonName);
            if (jString.Length != 0)
            {
                jDataList = JsonMapper.ToObject(jString);
            }
        }
    }
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        jsonName = Application.persistentDataPath + StringPathsInfo.LEADERS_jsonName;
    }
    void Start()
    {
        InitJData();
        #region CREATE_DATA
        int size = jDataList.Count;
        PlayerStats[] leaders = new PlayerStats[size];
        childrenRecords = new GameObject[size];
        #endregion
        gameObject.SetRectTransformer(0.0f, -size * 0.05f, 1.0f, 1.0f);
        
        for (int index = 0; index < size; index++)
        {
            leaders[index] = new PlayerStats(index, jsonName);
            childrenRecords[index] = Instantiate(prefabToInstatiate) as GameObject;
            #region FILL_DATA
            FillText(0, index.ToString(), childrenRecords[index]);
            FillText(1, leaders[index].Name, childrenRecords[index]);
            FillText(2, leaders[index].Score.ToString(), childrenRecords[index]);
            FillText(3, leaders[index].Level.ToString(), childrenRecords[index]);
            #endregion
        }
        ArrayList tmpRecords = new ArrayList(childrenRecords);
        tmpRecords.Reverse();
        for (int index = 0; index < tmpRecords.Count; index++ )
        {
            childrenRecords[index] = (GameObject)tmpRecords[index];
        }
        childrenRecords.SetParentToObjects(this.gameObject);
    }
    #endregion
    #region LOGIC
    private void FillText(int index, string whatToFill, GameObject parent)
    {
        parent.GetChilds()[index].GetComponent<Text>().text = whatToFill;
    }
    #endregion

}
