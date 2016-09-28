using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class GUIBulletManager : MonoBehaviour
{
    #region COMPONENTS_TO_SET
    [Header(StringHeadersInfo.GUIELEMENT_Header)]
    public GameObject[] guiElements;
    #endregion
    #region COMPONENTS_TO_CACHE
    private Text[] textElements;
    #endregion
    #region STANDART_EVENTS
    void Awake()
    {
        textElements = new Text[guiElements.Length];
        for (int index = 0; index != guiElements.Length; index++)
        {
            textElements[index] = guiElements[index].GetComponent<Text>();
        }
    }
    #endregion
    #region LOGIC
    public void SetInfo(string infoToGet)
    {
        foreach (Text textElement in textElements)
        {
            textElement.text = infoToGet;
        }
    }
    #endregion
}
