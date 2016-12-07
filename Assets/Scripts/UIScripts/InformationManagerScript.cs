using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InformationManagerScript : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public GameObject[] childs = null;
    public Manager fillerManager;
    public float sizeOfRecord = 0.05f;
    void Start()
    {
        fillerManager = GetComponent<Manager>();
        childs = new GameObject[fillerManager.Size()];
        gameObject.SetRectTransformer(0.0f, -fillerManager.Size() * sizeOfRecord, 1.0f, 1.0f);
        SetTextSizeForChildTexts();
        for (int index = 0; index < fillerManager.Size(); index++)
        {
            childs[index] = Instantiate(prefabToInstantiate) as GameObject;
        }
        childs.SetParentToObjects(gameObject);
    }
    private void SetTextSizeForChildTexts()
    {
        Text[] childrenOfPrefab = prefabToInstantiate.GetComponentsInChildren<Text>();
        foreach (Text textComponent in childrenOfPrefab)
        {
            textComponent.resizeTextMaxSize = (int)(SetCanvasScaler.baseFont * SetCanvasScaler.scale);
        }
    }
}
