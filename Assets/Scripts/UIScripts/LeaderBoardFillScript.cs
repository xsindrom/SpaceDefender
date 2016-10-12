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
    public static int[] fontSize = null;
    void InitJData()
    {
        jDataList = new JsonData();
        jDataList.SetJsonType(JsonType.Array);
        string jString = File.ReadAllText(jsonName);
        if (jString.Length != 0)
        {
            jDataList = JsonMapper.ToObject(jString);
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

        #region SetFontSize_to_Prefab
        Text[] childrenOfPrefab = new Text[prefabToInstatiate.GetChilds().Length];
        childrenOfPrefab.CacheComponents<Text>(prefabToInstatiate.GetChilds());
        if (fontSize == null)
        {
            fontSize = new int[childrenOfPrefab.Length];
            for (int index = childrenOfPrefab.Length - 1; index > -1; index--)
            {
                fontSize[index] = childrenOfPrefab[index].resizeTextMaxSize;
            }
        }
        for (int index = childrenOfPrefab.Length - 1; index > -1; index--)
        {
            childrenOfPrefab[index].resizeTextMaxSize = (int)(fontSize[index]*SetCanvasScaler.scale);
        }
        #endregion
        for (int index = 0; index < size; index++)
        {
            leaders[index] = new PlayerStats(index, jsonName);
            childrenRecords[index] = Instantiate(prefabToInstatiate) as GameObject;
            #region FILL_DATA
            FillText(0, (index + 1).ToString(), childrenRecords[index]);
            FillText(1, leaders[index].Name, childrenRecords[index]);
            FillText(2, leaders[index].Score.ToString(), childrenRecords[index]);
            FillText(3, leaders[index].Level.ToString(), childrenRecords[index]);
            #endregion
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
