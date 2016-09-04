using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//--Seems to be good---
public class GUIBulletManager : MonoBehaviour {

    [Header(StringHeadersInfo.GUIELEMENT_Header)]
    public GameObject[] guiElements;
    private Text[] textElements;
    void Awake()
    {
        textElements = new Text[guiElements.Length];
        for (int index = 0; index != guiElements.Length; index++)
        {
            textElements[index] = guiElements[index].GetComponent<Text>();
        }
    }
    public void SetInfo(string infoToGet)
    {
        foreach (Text textElement in textElements)
        {
            textElement.text = infoToGet;
        }
        
    }
}
